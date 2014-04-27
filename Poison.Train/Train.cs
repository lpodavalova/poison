using Poison.Stochastic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poison.Modelling;

namespace Poison.Train
{
    public class Train
    {
        /*
        const string generator1Name = "g1";
        const string queue1Name = "q1";
        const string facility1Name = "f1";

        const string generator2Name = "g2";
        const string queue2Name = "q2";
        const string facility2Name = "f2";

        private pm.Model _Model;

        public Train()        
        {
            _Model = new pm.Model();

            _Model.Generators.Add(new pm.Generator(generator1Name, new Normal(5, 2), G1EntryPoint));
            _Model.Queues.Add(new pm.Queue(queue1Name));
            _Model.Facilities.Add(new pm.Facility(facility1Name));

            _Model.Generators.Add(new pm.Generator(generator2Name, new Normal(4, 2), G2EntryPoint));
            _Model.Queues.Add(new pm.Queue(queue2Name));
            _Model.Facilities.Add(new pm.Facility(facility2Name));
        }

        public string Simulate()
        {
            _Model.Simulate(100);

            return GetStatistics();
        }

        private string GetStatistics()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("START TIME: {0}", 0.0);
            builder.AppendLine();
            builder.AppendFormat("END TIME: {0}", _Model.Time);
            builder.AppendLine();
            builder.AppendFormat("FACILITIES: {0}", _Model.Facilities.Count);
            builder.AppendLine();
            builder.AppendFormat("STORAGES: {0}", 0);
            builder.AppendLine();
            builder.AppendLine();

            builder.AppendFormat("FACILITIES");
            builder.AppendLine();
            builder.AppendLine();

            foreach (pm.Facility facility in _Model.Facilities)
            {
                builder.AppendFormat("FACILITY NAME: {0}", facility.Name);
                builder.AppendLine();
                builder.AppendFormat("FACILITY ENTRIES: {0}", facility.Entries);
                builder.AppendLine();
                builder.AppendFormat("FACILITY UTIL: {0}", facility.Utilization);
                builder.AppendLine();
                builder.AppendFormat("FACILITY AVE . TIME: {0}", facility.AverageTime);
                builder.AppendLine();
                builder.AppendFormat("FACILITY AVAIL: {0}", facility.State == pm.Enums.FacilityState.Free ? "Yes" : "No");
                builder.AppendLine();
                builder.AppendFormat("FACILITY OWNER: {0}", facility.LastOwner);
                builder.AppendLine();
                builder.AppendLine();
            }

            builder.AppendLine();

            builder.AppendFormat("QUEUES");
            builder.AppendLine();
            builder.AppendLine();

            foreach (pm.Queue queue in _Model.Queues)
            {
                builder.AppendFormat("QUEUE NAME: {0}", queue.Name);
                builder.AppendLine();
                builder.AppendFormat("QUEUE MAX: {0}", queue.Max);
                builder.AppendLine();
                builder.AppendFormat("QUEUE CONT.: {0}", queue.Count);
                builder.AppendLine();
                builder.AppendFormat("QUEUE ENTRY: {0}", queue.EntryCount);
                builder.AppendLine();
                builder.AppendFormat("QUEUE ENTRY (0): {0}", queue.EntryCountZero);
                builder.AppendLine();
                builder.AppendFormat("QUEUE AVE. CONT.: {0}", queue.AverageCount);
                builder.AppendLine();
                builder.AppendFormat("QUEUE AVE. TIME: {0}", queue.AverageTime);
                builder.AppendLine();
                builder.AppendFormat("QUEUE AVE. TIME (-0): {0}", queue.AverageTimeNonZero);
                builder.AppendLine();
                builder.AppendLine();
            }

            builder.AppendLine();

            return builder.ToString();
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
         */
    }
}
