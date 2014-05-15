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

using Poison.Modelling;
using Poison.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poison.Statistics
{
    public class FacilityStat
    {
        private Facility _Facility;
        private ModelStat _ModelStat;

        public FacilityStat(ModelStat modelStat, Facility facility)
        {
            if (modelStat == null)
            {
                throw new ArgumentNullException("modelStat");
            }

            if (facility == null)
            {
                throw new ArgumentNullException("facility");
            }

            _ModelStat = modelStat;
            _Facility = facility;

            facility.Initialization += facility_Initialization;
            facility.Finalization += facility_Finalization;
        }

        private void facility_Initialization(Facility facility)
        {
            Entries = 0;
            seizeTime = 0.0;
            LastOwner = null;
        }

        private void facility_Finalization(Facility facility)
        {
            if (facility.State != FacilityState.Free)
            {
                seizeTime += facility.Model.Time - timeStart;
            }
        }

        public int Entries
        {
            get;
            internal set;
        }

        public double AverageTime
        {
            get
            {
                return seizeTime.SmartDiv(Entries);
            }
        }

        public double Utilization
        {
            get
            {
                return seizeTime / _Facility.Model.Time;
            }
        }

        public Transact LastOwner
        {
            get;
            private set;
        }

        private double seizeTime;
        private double timeStart;
    }
}
