using System;
using System.Collections.Generic;
using System.Text;
using Framework.Domain;

namespace HomeCleaning.Domain
{
    public class CleaningExtraOption : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int CleaningCategoryId { get; set; }

        public CleaningCategory CleaningCategory { get; set; }
    }
}
