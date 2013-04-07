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

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return ((IEnumerable<T>)queue).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)queue).GetEnumerator();
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
