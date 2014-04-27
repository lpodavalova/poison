using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poison.Modelling
{
    public class ReadOnlyModelEntityCollection<T> : IEnumerable<T>, IEnumerable where T : IModelEntity
    {
        private ModelEntityCollection<T> modelEntityCollection;

        public ReadOnlyModelEntityCollection(ModelEntityCollection<T> modelEntityCollection)
        {
            if (modelEntityCollection == null)
            {
                throw new ArgumentNullException("modelEntityCollection");
            }

            this.modelEntityCollection = modelEntityCollection;
        }

        public T this[string key]
        {
            get
            {
                return modelEntityCollection[key];
            }
        }

        public void Add(T item)
        {
            throw new NotSupportedException("Trying to modify readonly collection.");
        }

        public void Clear()
        {
            throw new NotSupportedException("Trying to modify readonly collection.");
        }

        public bool ContainsName(string name)
        {
            return modelEntityCollection.ContainsName(name);
        }

        public int Count
        {
            get 
            {
                return modelEntityCollection.Count;
            }
        }

        public bool Remove(string name)
        {
            throw new NotSupportedException("Trying to modify readonly collection.");
        }

        public IEnumerator<T> GetEnumerator()
        {
            return modelEntityCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
