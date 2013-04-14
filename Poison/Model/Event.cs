/*
* The Poison: discrete event simulation system.
* Copyright (C) 2006-2013 Poison team.
*
* This program is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.
*
* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
* GNU General Public License for more details.
*
* You should have received a copy of the GNU General Public License
* along with this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;

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
