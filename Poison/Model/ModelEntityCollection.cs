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
            return GetEnumerator();
        }
    }
}
