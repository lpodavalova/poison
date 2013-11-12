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

            Console.WriteLine("START TIME: {0}", 0.0);
            Console.WriteLine("END TIME: {0}", model.Time);
            Console.WriteLine("FACILITIES: {0}", model.Facilities.Count);
            Console.WriteLine("STORAGES: {0}", 0);
            Console.WriteLine();

            Console.WriteLine("FACILITIES");
            Console.WriteLine();

            foreach (pm.Facility facility in model.Facilities)
            {
                Console.WriteLine("FACILITY NAME: {0}", facility.Name);
                Console.WriteLine("FACILITY ENTRIES: {0}", facility.Entries);
                Console.WriteLine("FACILITY UTIL: {0}", facility.Utilization);
                Console.WriteLine("FACILITY AVE . TIME: {0}", facility.AverageTime);
                Console.WriteLine("FACILITY AVAIL: {0}", facility.State == pm.Enums.FacilityState.Free ? "Yes" : "No");
                Console.WriteLine("FACILITY OWNER: {0}", facility.LastOwner);               
                Console.WriteLine();
            }

            Console.WriteLine();

            Console.WriteLine("QUEUES");
            Console.WriteLine();

            foreach (pm.Queue queue in model.Queues)
            {
                Console.WriteLine("QUEUE NAME: {0}", queue.Name);
                Console.WriteLine("QUEUE MAX: {0}", queue.Max);
                Console.WriteLine("QUEUE CONT.: {0}", queue.Count);
                Console.WriteLine("QUEUE ENTRY: {0}", queue.EntryCount);
                Console.WriteLine("QUEUE ENTRY (0): {0}", queue.EntryCountZero);
                Console.WriteLine("QUEUE AVE. CONT.: {0}", queue.AverageCount);
                Console.WriteLine("QUEUE AVE. TIME: {0}", queue.AverageTime);
                Console.WriteLine("QUEUE AVE. TIME (-0): {0}", queue.AverageTimeNonZero);                
                Console.WriteLine();
            }

            Console.WriteLine();

            Console.ReadLine();
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
