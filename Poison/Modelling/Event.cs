/*
* The Poison: discrete event simulation system.
* Copyright (C) 2006-2014 Poison team.
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

namespace Poison.Modelling
{
    class Event : IEquatable<Event>, IComparable<Event>
    {
        public Event(double time, EventHandler handler)
        {
            if (Math.Sign(time) < 0)
            {
                throw new ArgumentException("Parameter `time` cannot be less than zero.");
            }

            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            Time = time;
            Handler = handler;
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
            return Equals(objA, objB);
        }

        public static bool operator !=(Event objA, Event objB)
        {
            return !Equals(objA, objB);
        }

        public static bool operator <(Event objA, Event objB)
        {
            return Compare(objA, objB) < 0;
        }

        public static bool operator >(Event objA, Event objB)
        {
            return Compare(objA, objB) > 0;
        }

        public static bool operator >=(Event objA, Event objB)
        {
            return Compare(objA, objB) > -1;
        }

        public static bool operator <=(Event objA, Event objB)
        {
            return Compare(objA, objB) < 1;
        }

        public static bool Equals(Event objA, Event objB)
        {
            if ((object)objA == null)
            {
                if ((object)objB == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return objA.Equals(objB);
        }

        public static int Compare(Event objA, Event objB)
        {
            if (objA == null)
            {
                if (objB == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }

            return objA.CompareTo(objB);
        }

        public override bool Equals(object obj)
        {
            Event ev = obj as Event;

            return Equals(ev);
        }

        public override int GetHashCode()
        {
            return Time.GetHashCode();
        }

        public bool Equals(Event other)
        {
            if (other == null)
            {
                return false;
            }

            return Time.Equals(other.Time);
        }

        public int CompareTo(Event other)
        {
            if (other == null)
            {
                return 1;
            }

            return Time.CompareTo(other.Time);
        }
    }
}
