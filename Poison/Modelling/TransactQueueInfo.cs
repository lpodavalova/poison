﻿/*
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
using System.Linq;
using System.Text;

namespace Poison.Modelling
{
    internal class TransactQueueInfo
    {
        public Transact Transact
        {
            get;
            private set;
        }

        public double QueuingTime
        {
            get;
            private set;
        }

        public TransactQueueInfo(Transact transact, double queuingTime)
        {
            Transact = transact;
            QueuingTime = queuingTime;
        }
    }
}
