using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poison.Model
{
    public class Transact : IEquatable<Transact>, IComparable<Transact>
    {
        internal Transact(Model model, string generatorKey)
        {
            throw new NotImplementedException();
        }

        internal Transact(Model model, string generatorKey, int priority)
        {
            throw new NotImplementedException();
        }

        public Model Model
        {
            get;
            internal set;
        }

        public string GeneratorKey
        {
            get;
            internal set;
        }

        public Guid ID
        {
            get;
            private set;
        }

        public int Priority
        {
            get;
            private set;
        }

        public static bool operator ==(Transact objA, Transact objB)
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(Transact objA, Transact objB)
        {
            throw new NotImplementedException();
        }

        public static bool operator <(Transact objA, Transact objB)
        {
            throw new NotImplementedException();
        }

        public static bool operator >(Transact objA, Transact objB)
        {
            throw new NotImplementedException();
        }

        public static bool operator >=(Transact objA, Transact objB)
        {
            throw new NotImplementedException();
        }

        public static bool operator <=(Transact objA, Transact objB)
        {
            throw new NotImplementedException();
        }

        public static bool Equals(Transact objA, Transact objB)
        {
            throw new NotImplementedException();
        }

        public static int Compare(Transact objA, Transact objB)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public bool Equals(Transact other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(Transact other)
        {
            throw new NotImplementedException();
        }
    }
}
