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
        internal Transact(Model model, Generator generator)
        {
            throw new NotImplementedException();
        }

        internal Transact(Model model, Generator generator, int priority)
        {
            throw new NotImplementedException();
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
