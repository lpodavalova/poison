using Poison.Model.Enums;
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

            pm.Generator generator = new pm.Generator(generator1Name, new Normal(5, 2));
            generator.Entered += G1EntryPoint;

            model.Generators.Add(generator);
            pm.Queue q = new pm.Queue(queue1Name);
            q.NewItem += OnNewItemQ1;
            model.Queues.Add(q);
            pm.Facility f = new pm.Facility(facility1Name);
            f.Released += OnReleasedF1;
            model.Facilities.Add(f);

            generator = new pm.Generator(generator2Name, new Normal(4, 2));
            model.Generators.Add(generator);
            generator.Entered += G2EntryPoint;
            q = new pm.Queue(queue2Name);
            q.NewItem += OnNewItemQ2;
            model.Queues.Add(q);
            f = new pm.Facility(facility2Name);
            f.Released += OnReleasedF2;
            model.Facilities.Add(f);

            model.Simulate(1000000);

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

        private static void OnReleasedF1(pm.Model model, pm.Transact transact)
        {
            pm.Transact transactFromQueue = model.Queues[queue1Name].Dequeue();

            if (transactFromQueue != null)
            {
                SeizeF1(model.Facilities[facility1Name], transact);
            }

            model.Terminate(1);
        }

        private static void OnReleasedF2(pm.Model model, pm.Transact transact)
        {
            pm.Transact transactFromQueue = model.Queues[queue2Name].Dequeue();

            if (transactFromQueue != null)
            {
                SeizeF2(model.Facilities[facility2Name], transact);
            }

            model.Terminate(1);
        }

        private static void OnNewItemQ1(pm.Model model, pm.Transact transact)
        {
            if (model.Facilities[facility1Name].State == FacilityState.Free)
            {
                SeizeF1(model.Facilities[facility1Name], transact);
            }
        }

        private static void SeizeF1(pm.Facility facility, pm.Transact transact)
        {
            facility.Seize(transact, 15.0);
        }

        private static void SeizeF2(pm.Facility facility, pm.Transact transact)
        {
            facility.Seize(transact, 1.0);
        }

        private static void OnNewItemQ2(pm.Model model, pm.Transact transact)
        {
            if (model.Facilities[facility2Name].State == FacilityState.Free)
            {
                SeizeF2(model.Facilities[facility2Name], transact);
            }
        }

        private static void G1EntryPoint(pm.Model model, pm.Transact transact)
        {
            model.Queues[queue1Name].Enqueue(transact);
        }

        private static void G2EntryPoint(pm.Model model, pm.Transact transact)
        {
            if (model.Queues.Count() <= 10)
            {
                model.Queues[queue2Name].Enqueue(transact);
            }
            else
            {
                model.Terminate(1);
            }
        }
    }
}
