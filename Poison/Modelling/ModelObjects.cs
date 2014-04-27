using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poison.Modelling
{
    public class ModelObjects
    {
        public QueueCollection Queues
        {
            get;
            private set;
        }

        public FacilityCollection Facilities
        {
            get;
            private set;
        }

        public GeneratorCollection Generators
        {
            get;
            private set;
        }

        public ModelObjects(Model model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            Queues = new QueueCollection(model);
            Facilities = new FacilityCollection(model);
            Generators = new GeneratorCollection(model);
        }
    }
}
