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
        private IDictionary<string, QueueStat> _QueueStatCollection = new Dictionary<string, QueueStat>();
        private IDictionary<string, GeneratorStat> _GeneratorStatCollection = new Dictionary<string, GeneratorStat>();
        private IDictionary<string, FacilityStat> _FacilityStatCollection = new Dictionary<string, FacilityStat>();

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

        public ModelStat(Model model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            QueueStatCollection = new ReadOnlyDictionary<string, QueueStat>(_QueueStatCollection);
            GeneratorStatCollection = new ReadOnlyDictionary<string, GeneratorStat>(_GeneratorStatCollection);
            FacilityStatCollection = new ReadOnlyDictionary<string, FacilityStat>(_FacilityStatCollection);

            foreach (Queue queue in model.Queues)
            {
                _QueueStatCollection[queue.Name] = new QueueStat(queue);
            }

            foreach (Facility facility in model.Facilities)
            {
                _FacilityStatCollection[facility.Name] = new FacilityStat(facility);
            }

            foreach (Generator generator in model.Generators)
            {
                _GeneratorStatCollection[generator.Name] = new GeneratorStat(generator);
            }

            model.Queues.Added += Queues_Added;
            model.Queues.Removed += Queues_Removed;
            model.Queues.Cleared += Queues_Cleared;

            model.Generators.Added += Generators_Added;
            model.Generators.Removed += Generators_Removed;
            model.Generators.Cleared += Generators_Cleared;

            model.Facilities.Added += Facilities_Added;
            model.Facilities.Removed += Facilities_Removed;
            model.Facilities.Cleared += Facilities_Cleared;
        }

        private void Facilities_Removed(ModelEntityCollection<Facility> facilityCollection, Facility facility)
        {
            RemoveFacility(facility.Name);
        }

        private void Facilities_Added(ModelEntityCollection<Facility> facilityCollection, Facility facility)
        {
            _FacilityStatCollection[facility.Name] = new FacilityStat(facility);
        }

        private void Facilities_Cleared(ModelEntityCollection<Facility> obj)
        {
            foreach (string facilityName in _FacilityStatCollection.Keys.ToArray())
            {
                RemoveFacility(facilityName);
            }
        }

        private void RemoveFacility(string facilityName)
        {
            FacilityStat oldFacilityStat = _FacilityStatCollection[facilityName];

            string newName = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", facilityName, Guid.NewGuid());

            _FacilityStatCollection.Remove(facilityName);

            _FacilityStatCollection[newName] = oldFacilityStat;
        }

        private void Generators_Removed(ModelEntityCollection<Generator> generatorCollection, Generator generator)
        {
            RemoveGenerator(generator.Name);
        }

        private void Generators_Added(ModelEntityCollection<Generator> generatorCollection, Generator generator)
        {
            _GeneratorStatCollection[generator.Name] = new GeneratorStat(generator);
        }

        private void Generators_Cleared(ModelEntityCollection<Generator> obj)
        {
            foreach (string generatorName in _GeneratorStatCollection.Keys.ToArray())
            {
                RemoveGenerator(generatorName);
            }
        }

        private void RemoveGenerator(string generatorName)
        {
            GeneratorStat oldGeneratorStat = _GeneratorStatCollection[generatorName];

            string newName = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", generatorName, Guid.NewGuid());

            _GeneratorStatCollection.Remove(generatorName);

            _GeneratorStatCollection[newName] = oldGeneratorStat;
        }

        private void Queues_Removed(ModelEntityCollection<Queue> queueCollection, Queue queue)
        {
            RemoveQueue(queue.Name);
        }

        private void Queues_Added(ModelEntityCollection<Queue> queueCollection, Queue queue)
        {
            _QueueStatCollection[queue.Name] = new QueueStat(queue);
        }

        private void Queues_Cleared(ModelEntityCollection<Queue> queueCollection)
        {
            foreach (string queueName in _QueueStatCollection.Keys.ToArray())
            {
                RemoveQueue(queueName);
            }
        }

        private void RemoveQueue(string queueName)
        {
            QueueStat oldQueueStat = _QueueStatCollection[queueName];

            string newName = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", queueName, Guid.NewGuid());

            _QueueStatCollection.Remove(queueName);

            _QueueStatCollection[newName] = oldQueueStat;
        }
    }
}
