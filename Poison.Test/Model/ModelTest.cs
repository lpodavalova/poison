/*
* The Poison: discrete event simulation system.
* Copyright (C) 2006-2013 Poison team.
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poison.Model.Enums;
using pm = Poison.Model;
using ps = Poison.Stochastic;

namespace Poison.Test.Model
{
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        public void Test1()
        {
            pm.Model model = new pm.Model();

            model.Queues.Add(new pm.Queue("queue1"));
            model.Facilities.Add(new pm.Facility("facility1"));
            model.Generators.Add(new pm.Generator("generator1", new ps.Normal(10, 0.5)));

            model.Generators["generator1"].Entered += TransactHandler;
            model.Queues["queue1"].NewItem += OnNewItem;
            model.Facilities["facility1"].Released += OnReleased;

            model.Simulate(10000);

            Assert.Fail("Not implemented");
        }

        public void TransactHandler(pm.Model model, pm.Transact transact)
        {
            model.Queues["queue1"].Enqueue(transact);
        }

        private void OnNewItem(pm.Model model, pm.Transact transact)
        {
            pm.Facility facility = model.Facilities["facility1"];
            if (facility.State == FacilityState.Free)
            {
                SeizeFacility(facility,transact);
            }
        }

        private void OnReleased(pm.Model model, pm.Transact transact)
        {
            pm.Transact transactFromQueue = model.Queues["queue1"].Dequeue();

            if (transactFromQueue != null)
            {
                SeizeFacility(model.Facilities["facility1"],transactFromQueue);
            }

            model.Terminate(1);
        }

        private void SeizeFacility(pm.Facility facility, pm.Transact transact)
        {
            facility.Seize(transact, facilitySeizeTime.Next());
        }

        private ps.Normal facilitySeizeTime = new ps.Normal(6, 3);
    }
}
