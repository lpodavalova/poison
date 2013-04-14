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
    public class Transact : IEquatable<Transact>, IComparable<Transact>
    {
        internal Transact(Model model, Generator generator, int priority = 0)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }

            Priority = priority;
            Model = model;
            Generator = generator;
            ID = Guid.NewGuid();
        }

        public Model Model
        {
            get;
            internal set;
        }

        public Generator Generator
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
            return Equals(objA, objB);
        }

        public static bool operator !=(Transact objA, Transact objB)
        {
            return !Equals(objA, objB);
        }

        public static bool operator <(Transact objA, Transact objB)
        {
            return Compare(objA, objB) < 0;
        }

        public static bool operator >(Transact objA, Transact objB)
        {
            return Compare(objA, objB) > 0;
        }

        public static bool operator >=(Transact objA, Transact objB)
        {
            return Compare(objA, objB) > -1;
        }

        public static bool operator <=(Transact objA, Transact objB)
        {
            return Compare(objA, objB) < 1;
        }

        public static bool Equals(Transact objA, Transact objB)
        {
            if (objA == null)
            {
                if (objB == null)
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

        public static int Compare(Transact objA, Transact objB)
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
            return ID.GetHashCode();
        }

        public bool Equals(Transact other)
        {
            if (other == null)
            {
                return false;
            }

            return ID.Equals(other.ID);
        }

        public int CompareTo(Transact other)
        {
            if (other == null)
            {
                return 1;
            }

            return ID.CompareTo(other.ID);
        }
    }
}
