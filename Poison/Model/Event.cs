using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poison.Model
{
    class Event : IEquatable<Event>, IComparable<Event>
    {
        public Event(double time, EventHandler handler)
        {
            throw new NotImplementedException();
        }

        public double Time
        {
            get;
            private set;
        }

        public EventHandler Handler
        {
            get;
            private set;
        }

        public static bool operator ==(Event objA, Event objB)
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(Event objA, Event objB)
        {
            throw new NotImplementedException();
        }

        public static bool operator <(Event objA, Event objB)
        {
            throw new NotImplementedException();
        }

        public static bool operator >(Event objA, Event objB)
        {
            throw new NotImplementedException();
        }

        public static bool operator >=(Event objA, Event objB)
        {
            throw new NotImplementedException();
        }

        public static bool operator <=(Event objA, Event objB)
        {
            throw new NotImplementedException();
        }

        public static bool Equals(Event objA, Event objB)
        {
            throw new NotImplementedException();
        }

        public static int Compare(Event objA, Event objB)
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

        public bool Equals(Event other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(Event other)
        {
            throw new NotImplementedException();
        }
    }
}
