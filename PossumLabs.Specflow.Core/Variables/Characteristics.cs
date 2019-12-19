using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PossumLabs.Specflow.Core.Variables
{
    public class Characteristics : List<String>, IComparable<Characteristics>, IEquatable<Characteristics>
    {
        static Characteristics()
        {
            None = new Characteristics();
        }

        public static Characteristics None{get;}
        public Characteristics(params string[] items):base(items)
        {

        }

        public int CompareTo(Characteristics other)
        {
            var meNotOther = this.Except(other).ToList();
            var otherNotMe = other.Except(this).ToList();

            // not enough
            if (meNotOther.Any())
                return -1 * meNotOther.Count();
            // too many
            if (otherNotMe.Any())
                return 1 * otherNotMe.Count();

            // just right
            return 0;
        }

        public bool Equals(Characteristics other)
            => CompareTo(other) == 0;
    }
}
