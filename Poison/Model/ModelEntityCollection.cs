using System;
using System.Collections.Generic;
using System.Collections;

namespace Poison.Model
{
    public class ModelEntityCollection<T> : IEnumerable<T>, IEnumerable where T : IModelEntity 
    {
        private Model model;
        private Dictionary<string, T> dictionary;

        public ModelEntityCollection(Model model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            this.model = model;
            dictionary = new Dictionary<string, T>();
        }

        public T this[string key]
        {
            get
            {
                return dictionary[key];
            }
        }

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            
            if (dictionary.ContainsKey(item.Name))
            {
                throw new ArgumentException("Item with specified name already exists");
            }

            if (item.Model != null)
            {
                throw new ArgumentException("Model field of adding item should be null");
            }

            dictionary.Add(item.Name, item);
            item.Model = model;
        }

        public void Clear()
        {
            foreach (T item in dictionary.Values)
            {
                item.Model = null;
            }

            dictionary.Clear();
        }

        public bool ContainsName(string name)
        {
            return dictionary.ContainsKey(name);
        }

        public int Count
        {
            get 
            {
                return dictionary.Count;
            }
        }

        public bool Remove(string name)
        {
            if (!dictionary.ContainsKey(name))
            {
                return false;
            }

            dictionary[name].Model = null;

            return dictionary.Remove(name);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return dictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dictionary.Values.GetEnumerator();
        }
    }
}
