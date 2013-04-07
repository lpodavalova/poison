using System;
using Poison.Model.Enums;

namespace Poison.Model
{
    public class Device : IModelEntity
    {
        public Device(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
        }

        public string Name
        {
            get;
            private set;
        }

        public Model Model
        {
            get;
            internal set;
        }

        string IModelEntity.Name
        {
            get
            {
                return Name;
            }
        }

        Model IModelEntity.Model
        {
            get
            {
                return Model;
            }
            set
            {
                Model = value;
            }
        }

        public DeviceState State
        {
            get;
            private set;
        }

        public void Seize(Transact transact)
        {
            throw new NotImplementedException();
        }

        public void Release(Transact transact)
        {
            throw new NotImplementedException();
        }
    }
}
