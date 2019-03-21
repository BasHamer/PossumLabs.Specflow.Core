using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PossumLabs.Specflow.Core.Variables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PossumLabs.Specflow.Core.FluidDataCreation
{
    public class SetupDriver<C> where C : SetupBase<C>
    {
        public SetupDriver(C setup)
        {
            Setup = setup;
            Comparer = new IgnoreCaseEqualityComparer();
        }
        public C Setup { get; }

        public void Processor(string fileContent)
        {
            var configuration = DeserializeToDictionaryOrList(fileContent);

            RecursiveSetup(Setup, configuration as Dictionary<string, object>);
        }

        private IgnoreCaseEqualityComparer Comparer { get; }


        public void RecursiveSetup(object setup, Dictionary<string, object> configuration)
        {
            var setupMembers = setup.GetType().GetValueMembers();

            var setupMethods = setup.GetType().GetMethods()
                .Select(x => new
                {
                    method = x,
                    attributes = x.GetCustomAttributes(typeof(WithCreatorAttribute), true).Cast<WithCreatorAttribute>().ToList(),
                    parameterTypes = x.GetParameters().Select(y => y.ParameterType).ToList()
                })
                //trying to be safe
                .Where(x =>
                    x.attributes.Any() &&
                    x.parameterTypes.Count == 3 &&
                    (x.parameterTypes[0] == typeof(int) || x.parameterTypes[0] == typeof(string)) &&
                    x.parameterTypes[1] == typeof(string) &&
                    x.parameterTypes[2].IsGenericType &&
                    x.parameterTypes[2].GenericTypeArguments.Length == 1
                    )
                //data for future processing
                .Select(x =>
                new
                {
                    method = x,
                    type = x.parameterTypes[2].GenericTypeArguments[0],
                    actionType = x.parameterTypes[2],
                    single = x.parameterTypes.First() == typeof(string),
                    jsonAttribute = x.attributes.First().Name
                })
                .ToList();

            var unmatched = configuration.Keys
                .Except(setupMembers.Select(sm => sm.Name), Comparer) //property setters
                .Except(setupMethods.Select(sm => sm.jsonAttribute), Comparer) //with setters
                .Except(new string[] { "var", "count", "template" }) //magic driver values
                .ToList();

            if (unmatched.Any())
                throw new Exception($"{configuration.GetType().Name} does not have the properties {unmatched.LogFormat()}");

            ProcessValueMembers(setup, configuration, setupMembers);

            foreach (var m in configuration.Keys.Where(m => setupMethods.Select(sm => sm.jsonAttribute)
                .Any(s => Comparer.Equals(s, m))))
            {
                var children = configuration[m] as List<object>;

                foreach (var child in children.Cast<Dictionary<string,object>>())
                {
                    bool? single = null;
                    string var = null;
                    string template = null;
                    int? count = null;

                    if (child.ContainsKey("var"))
                    {
                        single = true;
                        if (!typeof(string).IsAssignableFrom(child["var"].GetType()))
                            throw new NotSupportedException($"var has to be a string under {m}");
                        var = (string)child["var"];
                    }
                    if (child.ContainsKey("count"))
                    {
                        single = false;
                        if (!typeof(Int64).IsAssignableFrom(child["count"].GetType()))
                            throw new NotSupportedException($"count has to be a integer under {m}");
                        count = (int)Convert.ToInt32((Int64)child["count"]);
                    }

                    if (child.ContainsKey("template"))
                    {
                        if (!typeof(string).IsAssignableFrom(child["template"].GetType()))
                            throw new NotSupportedException($"template has to be a string under {m}");
                        template = (string)child["template"];
                    }

                    if (child.ContainsKey("var") && child.ContainsKey("count"))
                        throw new InvalidOperationException($"can't have both var and count under {m}");
                    if (!single.HasValue)
                        throw new InvalidOperationException($"need either var and count under {m}");

                    var sm = setupMethods.FirstOrDefault(s => Comparer.Equals(s.jsonAttribute, m) && s.single == single);

                    var mode = single == true ? "single" : "count";
                    if (sm == null)
                        throw new NotImplementedException($"{setup.GetType().Name} does not support the {mode} creation of {m}");

                    Action<dynamic> r = (i) => RecursiveSetup(i, child);

                    if (single == true)
                        sm.method.method.Invoke(setup, new object[] { var, template, r });
                    else
                        sm.method.method.Invoke(setup, new object[] { count, template, r });
                }
            }
        }

        private void ProcessValueMembers(object setup, Dictionary<string, object> configuration, IEnumerable<ValueMemberInfo> setupMembers)
        {
            foreach (var m in configuration.Keys.Where(m => setupMembers.Select(sm => sm.Name)
                            .Any(s => Comparer.Equals(s, m))))
            {
                var sm = setupMembers.First(s => Comparer.Equals(s.Name, m));
                if (typeof(IRepository).IsAssignableFrom(sm.Type))
                    continue;

                if (sm.Type.IsAssignableFrom(configuration[m].GetType()))
                {
                    sm.SetValue(setup, configuration[m]);
                }
                else if (typeof(IValueObjectSetup).IsAssignableFrom(sm.Type))
                {
                    throw new NotImplementedException();
                    //create object and recurse
                }
                else
                    throw new UnsupportedException($"Unable to determine how to move from {m} into {sm.Type.Name}");
            }
        }

        private object DeserializeToDictionaryOrList(string jo, bool isArray = false)
        {
            if (!isArray)
            {
                isArray = jo.Substring(0, 1) == "[";
            }
            if (!isArray)
            {
                var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(jo);
                var values2 = new Dictionary<string, object>();

                foreach (KeyValuePair<string, object> d in values)
                {
                    if (d.Value is JObject)
                    {
                        values2.Add(d.Key, DeserializeToDictionaryOrList(d.Value.ToString()));
                    }
                    else if (d.Value is JArray)
                    {
                        values2.Add(d.Key, DeserializeToDictionaryOrList(d.Value.ToString(), true));
                    }
                    else
                    {
                        values2.Add(d.Key, d.Value);
                    }
                }
                return values2;
            }
            else
            {
                var values = JsonConvert.DeserializeObject<List<object>>(jo);
                var values2 = new List<object>();
                foreach (var d in values)
                {
                    if (d is JObject)
                    {
                        values2.Add(DeserializeToDictionaryOrList(d.ToString()));
                    }
                    else if (d is JArray)
                    {
                        values2.Add(DeserializeToDictionaryOrList(d.ToString(), true));
                    }
                    else
                    {
                        values2.Add(d);
                    }
                }
                return values2;
            }
        }
    }
}
