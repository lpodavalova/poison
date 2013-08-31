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

using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            model.Generators.Add(new pm.Generator("generator1", new ps.Normal(10, 0.5), TransactHandler));            

            model.Simulate(1000);

            Assert.Fail("Not implemented");
        }

        private ps.Normal facilitySeizeTime = new ps.Normal(6, 3);

        public void TransactHandler(pm.Model model, pm.Transact transact)
        {           
            model.Queues["queue1"].Enqueue(transact, new pm.TransactHandler(TransactHandler1));
        }

        public void TransactHandler1(pm.Model model, pm.Transact transact)
        {
            model.Facilities["facility1"].Seize(transact, new pm.TransactHandler(TransactHandler2));
        }

        public void TransactHandler2(pm.Model model, pm.Transact transact)
        {
            model.Queues["queue1"].Dequeue(transact);
            model.Advance(facilitySeizeTime.Next(), transact, new pm.TransactHandler(TransactHandler3));
        }

        public void TransactHandler3(pm.Model model, pm.Transact transact)
        {
            model.Facilities["facility1"].Release(transact);

            model.Terminate(1);
        }
    }
}
