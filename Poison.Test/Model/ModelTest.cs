using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pm = Poison.Model;
using ps = Poison.Stochastic;

namespace Poison.Test.Model
{
    /// <summary>
    /// Summary description for ModelTest
    /// </summary>
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        public void Test1()
        {
            pm.Model model = new pm.Model();
            
            model.Queues.Add("queue1",new pm.Queue());
            model.Devices.Add("device1", new pm.Device());
            model.Generators.Add("generator1",new pm.Generator(new ps.Normal(4, 2), TransactHandler));            

            model.Simulate(1000);

            Assert.Fail("Not implemented");
        }

        private ps.Normal deviceSeizeTime = new ps.Normal(6, 3);

        public void TransactHandler(pm.Model model, pm.Transact transact)
        {
            model.Queues["queue1"].Enqueue(transact);
            model.Devices["devices1"].Seize(transact);
            model.Queues["queue1"].Dequeue(transact);
            model.Advance(deviceSeizeTime.Next());
            model.Devices["devices1"].Release(transact);

            model.Terminate(1);
        }
    }
}
