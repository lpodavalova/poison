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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Poison.Statistics
{
    public class ModelStat
    {
        private IDictionary<string, QueueStat> _QueueStatCollection;
        private IDictionary<string, GeneratorStat> _GeneratorStatCollection;
        private IDictionary<string, FacilityStat> _FacilityStatCollection;

        public IReadOnlyDictionary<string, QueueStat> QueueStatCollection
        {
            get;
            private set;
        }

        public IReadOnlyDictionary<string, GeneratorStat> GeneratorStatCollection
        {
            get;
            private set;
        }

        public IReadOnlyDictionary<string, FacilityStat> FacilityStatCollection
        {
            get;
            private set;
        }

        public Model Model
        {
            get;
            private set;
        }

        public ModelStat(Model model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            Model = model;

            _QueueStatCollection = new Dictionary<string, QueueStat>();
            _GeneratorStatCollection = new Dictionary<string, GeneratorStat>();
            _FacilityStatCollection = new Dictionary<string, FacilityStat>();

            QueueStatCollection = new ReadOnlyDictionary<string, QueueStat>(_QueueStatCollection);
            GeneratorStatCollection = new ReadOnlyDictionary<string, GeneratorStat>(_GeneratorStatCollection);
            FacilityStatCollection = new ReadOnlyDictionary<string, FacilityStat>(_FacilityStatCollection);

            model.Initialization += model_Initialization;
        }

        private void model_Initialization(Model model)
        {
            _QueueStatCollection.Clear();
            _FacilityStatCollection.Clear();
            _GeneratorStatCollection.Clear();

            foreach (Queue queue in model.Queues)
            {
                _QueueStatCollection[queue.Name] = new QueueStat(this,queue);
            }

            foreach (Facility facility in model.Facilities)
            {
                _FacilityStatCollection[facility.Name] = new FacilityStat(this,facility);
            }

            foreach (Generator generator in model.Generators)
            {
                _GeneratorStatCollection[generator.Name] = new GeneratorStat(this,generator);
            }
        }
    }
}
