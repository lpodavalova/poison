using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poison.Model.Types
{
    enum DeviceState
    {
        // Устройство свободно
        Free,
        // Устройство занято
        Busy,
        // Устройство захвачено
        Seized
    }
}
