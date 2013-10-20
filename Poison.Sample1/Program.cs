using Poison.Stochastic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pm = Poison.Model;

namespace Poison.Sample1
{
    class Program
    {
        const string generator1Name = "g1";
        const string queue1Name = "q1";
        const string facility1Name = "f1";

        const string generator2Name = "g2";
        const string queue2Name = "q2";
        const string facility2Name = "f2";

        static void Main(string[] args)
        {
            pm.Model model = new pm.Model();

            model.Generators.Add(new pm.Generator(generator1Name, new Normal(5, 2), G1EntryPoint));
            model.Queues.Add(new pm.Queue(queue1Name));
            model.Facilities.Add(new pm.Facility(facility1Name));

            model.Generators.Add(new pm.Generator(generator2Name, new Normal(4, 2), G2EntryPoint));
            model.Queues.Add(new pm.Queue(queue2Name));
            model.Facilities.Add(new pm.Facility(facility2Name));

            model.Simulate(100);
        }

        private static void G1EntryPoint(pm.Model model, pm.Transact transact)
        {
            model.Queues[queue1Name].Enqueue(transact, F1EntryPoint);
        }

        private static void F1EntryPoint(pm.Model model, pm.Transact transact)
        {
            model.Queues[queue1Name].Dequeue(transact);
            model.Facilities[facility1Name].Seize(transact, Advance1);
        }

        private static void Advance1(pm.Model model, pm.Transact transact)
        {
            model.Advance(5.0, transact, Terminate1);
        }

        private static void Terminate1(pm.Model model, pm.Transact transact)
        {
            model.Facilities[facility1Name].Release(transact);
            model.Terminate(1);
        }

        private static void G2EntryPoint(pm.Model model, pm.Transact transact)
        {
            if (model.Queues.Count() <= 10)
            {
                model.Queues[queue2Name].Enqueue(transact, F2EntryPoint);
            }
            else
                model.Queues[queue2Name].Enqueue(transact, Terminate2);
        }

        private static void Advance2(pm.Model model, pm.Transact transact)
        {
            model.Advance(1.0, transact, Terminate2);
        }

        private static void Terminate2(pm.Model model, pm.Transact transact)
        {
            model.Facilities[facility2Name].Release(transact);
            model.Terminate(1);
        }

        private static void F2EntryPoint(pm.Model model, pm.Transact transact)
        {
            model.Queues[queue2Name].Dequeue(transact);
            model.Facilities[facility2Name].Seize(transact, Advance2);
        }
    }
}
