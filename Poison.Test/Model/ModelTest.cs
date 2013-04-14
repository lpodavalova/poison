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

            model.Queues.Add(new pm.Queue("queue1"));
            model.Devices.Add(new pm.Device("device1"));
            model.Generators.Add(new pm.Generator("generator1", new ps.Normal(4, 2), TransactHandler));            

            model.Simulate(1000);

            Assert.Fail("Not implemented");
        }

        private ps.Normal deviceSeizeTime = new ps.Normal(6, 3);

        public void TransactHandler(pm.Model model, pm.Transact transact)
        {           
            model.Queues["queue1"].Enqueue(transact, new pm.TransactHandler(TransactHandler1));
        }

        public void TransactHandler1(pm.Model model, pm.Transact transact)
        {
            model.Devices["devices1"].Seize(transact, new pm.TransactHandler(TransactHandler2));
        }

        public void TransactHandler2(pm.Model model, pm.Transact transact)
        {
            model.Queues["queue1"].Dequeue(transact);
            model.Advance(deviceSeizeTime.Next(), transact, new pm.TransactHandler(TransactHandler3));
        }

        public void TransactHandler3(pm.Model model, pm.Transact transact)
        {
            model.Devices["devices1"].Release(transact);

            model.Terminate(1);
        }
    }
}
