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
using System.Collections.Generic;
using System.Collections;

namespace Poison.Modelling
{
    public class ModelEntityCollection<T> : IEnumerable<T>, IEnumerable where T : IModelEntity 
    {
        private const string _CannotModifyCollectionMessage = "Cannot modify collection when it's locked.";

        private Model _Model;
        private Dictionary<string, T> _Dictionary;

        public bool Locked
        {
            get;
            internal set;
        }

        public ModelEntityCollection(Model model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            this._Model = model;
            _Dictionary = new Dictionary<string, T>();
            Locked = false;
        }

        public T this[string key]
        {
            get
            {
                return _Dictionary[key];
            }
        }      

        public void Add(T item)
        {
            if (Locked)
            {
                throw new InvalidOperationException(_CannotModifyCollectionMessage);
            }

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            
            if (_Dictionary.ContainsKey(item.Name))
            {
                throw new ArgumentException("Item with specified name already exists");
            }

            if (item.Model != null)
            {
                throw new ArgumentException("Model field of adding item should be null");
            }

            _Dictionary.Add(item.Name, item);
            item.Model = _Model;
        }

        public void Clear()
        {
            if (Locked)
            {
                throw new InvalidOperationException(_CannotModifyCollectionMessage);
            }

            foreach (T item in _Dictionary.Values)
            {
                item.Model = null;
            }

            _Dictionary.Clear();
        }

        public bool ContainsName(string name)
        {
            return _Dictionary.ContainsKey(name);
        }

        public int Count
        {
            get 
            {
                return _Dictionary.Count;
            }
        }

        public bool Remove(string name)
        {
            if (Locked)
            {
                throw new InvalidOperationException(_CannotModifyCollectionMessage);
            }

            if (!_Dictionary.ContainsKey(name))
            {
                return false;
            }

            T item = _Dictionary[name];

            item.Model = null;

            bool result = _Dictionary.Remove(name);

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _Dictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
