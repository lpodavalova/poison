using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poison.Model.Types;

namespace Poison.Model
{
    /// <summary>
    /// Устройство
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Состояние устройства
        /// </summary>
        public DeviceState State
        {
            get;
            private set;
        }

        /// <summary>
        /// Память устройства
        /// </summary>
        public int Memory
        {
            get;
            private set;
        }

        /// <summary>
        /// Размер памяти устройства
        /// </summary>
        public int MemorySize
        {
            get;
            private set;
        }

        /// <summary>
        /// Список транзактов, обслуживаемых
        /// устройством
        /// </summary>
        public List<Transact> Transacts
        {
            get;
            private set;
        }
    }
}
