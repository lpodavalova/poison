using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poison.Model.Enums;

namespace Poison.Model
{
    public class Device
    {
        public Device(string name)
        {
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
