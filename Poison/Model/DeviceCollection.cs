using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poison.Model
{
    public class DeviceCollection : IDictionary<string, Device>
    {
        public void Add(string key, Device value)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        public ICollection<string> Keys
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out Device value)
        {
            throw new NotImplementedException();
        }

        public ICollection<Device> Values
        {
            get { throw new NotImplementedException(); }
        }

        public Device this[string key]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(KeyValuePair<string, Device> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<string, Device> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<string, Device>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(KeyValuePair<string, Device> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, Device>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
