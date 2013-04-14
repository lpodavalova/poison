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

        public void Seize(Transact transact, TransactHandler transactHandler)
        {
            while (Model.IsAlive() && State != DeviceState.Free)
            {
                Model.ProcessEvent();
            }

            if (!Model.IsAlive())
            {
                return;
            }

            State = DeviceState.Busy;
            transactHandler(Model, transact);            
        }

        public void Release(Transact transact)
        {
            State = DeviceState.Free;
        }
    }
}
