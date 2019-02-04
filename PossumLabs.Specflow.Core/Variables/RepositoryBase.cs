using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace PossumLabs.Specflow.Core.Variables
{
    public class RepositoryBase<T> : IRepository, IEnumerable<KeyValuePair<string,T>>
        where T : IValueObject
    {
        public RepositoryBase(Interpeter interpeter, ObjectFactory objectFactory)
        {
            SetupDefaultConversions();
            Defaults = new Dictionary<string, string>();
            Interpeter = interpeter;
            ObjectFactory = objectFactory;
        }

        private Dictionary<string, IValueObject> dictionary = new Dictionary<string, IValueObject>();
        private List<TypeConverter> conversions = new List<TypeConverter>();
        private Interpeter Interpeter { get; }
        private ObjectFactory ObjectFactory { get; }

        public IValueObject this[string key] => dictionary[key];
        public Type Type => typeof(T);
        public IEnumerable<TypeConverter> RegisteredConversions => conversions;
        public Dictionary<string, string> Defaults { get; }

        public void Add(string key, IValueObject item) => dictionary.Add(key, item);
        public void Add(Dictionary<string,T> d) => d.Keys.ToList().ForEach(key=>dictionary.Add(key, d[key]));
        public bool ContainsKey(string root) => dictionary.ContainsKey(root);

        public IEnumerator<KeyValuePair<string, T>> GetEnumerator() 
            => dictionary.ToDictionary(x => x.Key, x => (T)x.Value).ToList().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() 
            => dictionary.ToDictionary(x => x.Key, x => (T)x.Value).ToList().GetEnumerator();

        public void RegisterConversion<C>(Func<C, T> conversion, Predicate<C> test) =>
            conversions.Add(new TypeConverter((x) => conversion.Invoke((C)x), x => test.Invoke((C)x)));

        protected virtual void SetupDefaultConversions()
        {
            RegisterConversion<object>(
                c => (T)c, 
                c => typeof(T).IsAssignableFrom(c.GetType()));

            RegisterConversion<string>(
                c => JsonConvert.DeserializeObject<T>((string)c), 
                c => typeof(string).IsAssignableFrom(c.GetType()) && ((string)c).IsValidJson());
        }

        public T Map(Dictionary<string, KeyValuePair<string, string>> values)
        {
            foreach (var key in Defaults.Keys)
            {
                if (!values.ContainsKey(key.ToUpper()))
                {
                    values.Add(key.ToUpper(), new KeyValuePair<string, string>($"default/{key}", Defaults[key]));
                }
            }
            return values.MapTo<T>(Interpeter, ObjectFactory);
        }

        public Dictionary<string, object> AsDictionary()
            => dictionary.ToDictionary(
                x => x.Key, 
                x => (object)x.Value);
    }
}
