using System;
using System.Collections.Generic;
using System.Text;
using Framework.Domain;

namespace HomeCleaning.Domain
{
    public class CleaningOption : Entity
    {
        public string Name { get; set; }

        public CleaningCategory CleaningCategory { get; set; }
    }
}
