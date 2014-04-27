using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poison.Modelling;
using Poison.Stochastic;

namespace Poison.Test.Modelling
{
    class TestModel : Model
    {
        private const string queue1 = "queue1";
        private const string generator1 = "generator1";
        private const string facility1 = "facility1";

        private int transactionCount = 10000000;
        private Normal facilitySeizeTime = new Normal(6, 3);

        protected override bool IsAlive()
        {
            return transactionCount >= 0;
        }

        protected override void Describe(ModelObjects modelObjects)
        {
            modelObjects.Queues.Add(new Queue(queue1));
            modelObjects.Facilities.Add(new Facility(facility1));
            modelObjects.Generators.Add(new Generator(generator1, new Normal(10, 0.5)));

            modelObjects.Generators[generator1].Entered += TestModel_Entered;
            modelObjects.Queues[queue1].Enqueued += TestModel_Enqueued;
            modelObjects.Facilities[facility1].Released += TestModel_Released;
        }

        private void TestModel_Entered(Transact transact)
        {
            Queues[queue1].Enqueue(transact);
        }

        private void TestModel_Enqueued(Queue obj)
        {
            if (Facilities[facility1].State == FacilityState.Free)
            {
                SeizeFacility(Facilities[facility1], Queues[queue1].Dequeue());
            }
        }

        private void TestModel_Released(Facility obj)
        {
            if (!Queues[queue1].Empty)
            {
                SeizeFacility(Facilities[facility1], Queues[queue1].Dequeue());
            }

            transactionCount--;
        }

        private void SeizeFacility(Facility facility, Transact transact)
        {
            facility.Seize(transact);
            Advance(Math.Abs(facilitySeizeTime.Next()), ReleaseFacility);
        }

        private void ReleaseFacility()
        {
            Facilities[facility1].Release();
        }
    }
}
