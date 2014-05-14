/*
* The Poison: discrete event simulation system.
* Copyright (C) 2013-2014 Poison team.
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
using System.Collections;
using System.Collections.Generic;

namespace Poison.Collections
{
    class PriorityQueue<T> : IEnumerable<T>, ICollection, IEnumerable where T : IComparable<T>
    {
        private List<T> _Queue;

        public PriorityQueue()
        {
            _Queue = new List<T>();
        }

        public PriorityQueue(int capacity)
        {
            _Queue = new List<T>(capacity);
        }

        public void Clear()
        {
            _Queue.Clear();
        }

        public bool Contains(T item)
        {
            return _Queue.Contains(item);
        }

        public void Enqueue(T item)
        {
            int index = _Queue.BinarySearch(item);

            if (index < 0)
            {
                index = ~index;
            }

            _Queue.Insert(index, item);
        }

        public T Dequeue()
        {
            T item = Peek();

            _Queue.RemoveAt(0);

            return item;
        }

        public T Peek()
        {
            if (_Queue.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            return _Queue[0];
        }

        public T[] ToArray()
        {
            return _Queue.ToArray();
        }

        public void TrimExcess()
        {
            _Queue.TrimExcess();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_Queue).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)_Queue).CopyTo(array, index);
        }

        public int Count
        {
            get
            {
                return _Queue.Count;
            }
        }

        bool ICollection.IsSynchronized
        {
            get 
            {
                return ((ICollection)_Queue).IsSynchronized;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                return ((ICollection)_Queue).SyncRoot;
            }
        }
    }
}
