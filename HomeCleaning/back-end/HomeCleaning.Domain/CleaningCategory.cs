using System;

namespace HomeCleaning.Domain
{
    public class CleaningCategory : Entity, IAggregateRoot<CleaningCategory>
    {
        public string Name { get; set; }
    }
}
