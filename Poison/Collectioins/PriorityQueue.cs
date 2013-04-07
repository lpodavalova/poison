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
using System.Collections;
using System.Collections.Generic;

namespace Poison.Collections
{
    class PriorityQueue<T> : IEnumerable<T>, ICollection, IEnumerable where T : IComparable<T>
    {
        private List<T> queue;

        public PriorityQueue()
        {
            queue = new List<T>();
        }

        public PriorityQueue(int capacity)
        {
            queue = new List<T>(capacity);
        }

        public void Clear()
        {
            queue.Clear();
        }

        public bool Contains(T item)
        {
            return queue.Contains(item);
        }

        public void Enqueue(T item)
        {
            int index = queue.BinarySearch(item);

            if (index < 0)
            {
                index = ~index;
            }

            queue.Insert(index, item);
        }

        public T Dequeue()
        {
            T item = Peek();

            queue.RemoveAt(0);

            return item;
        }

        public T Peek()
        {
            if (queue.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            return queue[0];
        }

        public T[] ToArray()
        {
            return queue.ToArray();
        }

        public void TrimExcess()
        {
            queue.TrimExcess();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)queue).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)queue).CopyTo(array, index);
        }

        public int Count
        {
            get
            {
                return queue.Count;
            }
        }

        bool ICollection.IsSynchronized
        {
            get 
            {
                return ((ICollection)queue).IsSynchronized;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                return ((ICollection)queue).SyncRoot;
            }
        }
    }
}
