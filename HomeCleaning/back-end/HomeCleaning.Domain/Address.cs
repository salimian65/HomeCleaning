using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HomeCleaning.Domain
{
    public class Address : Entity
    {
        public City City { get; set; }

        public string Street { get; set; }

        public string Alley { get; set; }

        public string Floor { get; set; }

        public string Block { get; set; }
    }

    public class City
    {
    }

    public enum OrderStatus
    {
        Starting = 1,
        Confirmation = 2,
        Approved = 3,
        InProcessed = 4,
        Ended = 5
    }
}
