using Microsoft.VisualStudio.TestTools.UnitTesting;
using PossumLabs.Specflow.Core.Variables;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PossumLabs.Specflow.Core.UnitTests.Variables
{
    [TestClass]
    public class Interperter_Convert
    {
        private Interpeter Interpeter { get; }
        private ObjectFactory ObjectFactory { get; }

        public Interperter_Convert()
        {
            ObjectFactory = new ObjectFactory();
            Interpeter = new Interpeter(ObjectFactory);
        }

        [TestMethod]
        public void ReturnNullWhenObjectNull()
            =>Interpeter.Convert(typeof(MyDomainObject), null)
                .Should().BeNull("nulls should stay null if possible");
        



        ////public object Convert(Type t, object o)
        ////{
        ////    if (o == null)
        ////    {
        ////        if (t.IsValueType)
        ////            return ObjectFactory.CreateInstance(t);
        ////        return null;
        ////    }

        ////    var sourceType = o.GetType();

        ////    if (t.IsAssignableFrom(sourceType))
        ////        return o;

        ////    var conversions = Repositories.Where(x => x.Type == t).SelectMany(x => x.RegisteredConversions).Where(c => c.Test.Invoke(o));
        ////    if (conversions.Any())
        ////        return conversions.First().Conversion.Invoke(o);

        ////    //handle nullables
        ////    var targetType = t;
        ////    if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
        ////        targetType = Nullable.GetUnderlyingType(targetType);

        ////    var convertConversions = typeof(System.Convert).CachedGetMethods()
        ////        .Where(x => x.ReturnType == targetType && x.Name.StartsWith("To") && x.GetParameters().Any(p => p.ParameterType == sourceType));

        ////    if (convertConversions.Any())
        ////        return convertConversions.First().Invoke(null, o.AsObjectArray());

        ////    if (targetType.IsEnum && sourceType == typeof(string))
        ////        return Enum.Parse(targetType, o as string);

        ////    if (targetType.GetConstructors().Where(x => x.IsPublic && !x.IsStatic).Select(c => c.GetParameters())
        ////        .Any(x => x.One() && x.First().ParameterType == sourceType))
        ////        return Activator.CreateInstance(targetType, o);

        ////    var parseMethod = targetType.CachedGetMethods().Where(m => m.IsStatic && m.IsPublic)
        ////        .Where(m => m.Name == "Parse" && m.GetParameters().Length == 1 && m.GetParameters().First().ParameterType == sourceType);

        ////    if (parseMethod.Any())
        ////        return parseMethod.First().Invoke(null, o.AsObjectArray());

        ////    if (targetType == typeof(string))
        ////        return o.ToString();

        ////    if (sourceType == typeof(string) && ((string)o).IsValidJson())
        ////        return JsonConvert.DeserializeObject((string)o, t);

        ////    // generic lists
        ////    Type genericType = null;
        ////    foreach (Type interfaceType in targetType.GetInterfaces())
        ////    {
        ////        if (interfaceType.IsGenericType &&
        ////            interfaceType.GetGenericTypeDefinition()
        ////            == typeof(IList<>))
        ////        {
        ////            genericType = interfaceType.GetGenericArguments()[0];
        ////            break;
        ////        }
        ////    }
        ////    if (genericType != null)
        ////    {
        ////        var collection = (IList)Activator.CreateInstance(targetType);
        ////        if (sourceType == genericType || sourceType.IsInstanceOfType(genericType))
        ////            collection.Add(o);
        ////        else if (sourceType == typeof(string))
        ////            foreach (var expression in ((string)o).Split(',').Select(x => x.Trim()).Where(x => !String.IsNullOrEmpty(x)))
        ////                collection.Add(Get(genericType, expression));
        ////        return collection;
        ////    }

        ////    if (sourceType == typeof(string) && (string)o == "null")
        ////        return null;

        ////    throw new GherkinException($"Unable to convert from {sourceType} to {t}");
        ////}
    }

    public class InterperterUnitTest
    {
    }
}
