using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCleaning.Domain
{
    public class CleaningOption : Entity
    {
        public string Name { get; set; }

        public CleaningCategory CleaningCategory { get; set; }
    }
}
