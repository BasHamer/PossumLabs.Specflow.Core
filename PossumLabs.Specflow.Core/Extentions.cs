using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PossumLabs.Specflow.Core
{
    public static class Extentions
    {
        public static string LogFormatSeperator(this IEnumerable<string> l)
            => (l.Count() > 5 || (l.Max(x => x == null ? 0 : x.Length) > 25)) ? Environment.NewLine : ", ";
        
        public static string LogFormat(this IEnumerable<string> l)
            => l.Any() ? l.OrderBy(x => x.ToLower()).Aggregate((x, y) => x + LogFormatSeperator(l) + y) : string.Empty;

        public static string LogFormat(this IEnumerable<IEnumerable<string>> l)
            => l.Any() ? l.Select(x => x.LogFormat()).Aggregate((x, y) => x + "\n" + y) : string.Empty;

        public static string LogFormat<T>(this IEnumerable<T> l, Func<T, string> f)
            => l.Any() ? l.Select(x => f.Invoke(x)).LogFormat() : string.Empty;

        public static string LogFormat<T>(this IEnumerable<IEnumerable<T>> l, Func<T, string> f)
            => l.Any() ? l.Select(x => x.Select(y => f.Invoke(y))).LogFormat() : string.Empty;

        public static bool None<T>(this IEnumerable<T> l)
            => !l.Any();

        public static bool Many<T>(this IEnumerable<T> l)
            => l.Count() > 1;

        public static bool One<T>(this IEnumerable<T> l)
            => l.Count() == 1;

        public static bool None<T>(this IEnumerable<T> l, Func<T, bool> predicate)
            => !l.Any(predicate);

        public static bool Many<T>(this IEnumerable<T> l, Func<T, bool> predicate)
            => l.Where(predicate).Count() > 1;

        public static bool One<T>(this IEnumerable<T> l, Func<T, bool> predicate)
            => l.Where(predicate).Count() == 1;

        public static object[] AsObjectArray(this object i)
            => new object[] { i };

        public static IEnumerable ConvertToIEnumerable(this object o)
        {
            if (o == null)
                throw new Exception($"The referenced variable does not have a value.");
            if (o is IEnumerable)
                return ((IEnumerable)o);
            throw new GherkinException("The referenced variable is not enumerable.");
        }

        public static string RelativeFrom(this FileInfo file, DirectoryInfo directory)
            => new Uri(directory.FullName).MakeRelativeUri(new Uri(file.FullName)).ToString();
    }
}
