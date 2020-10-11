using System;
using System.ComponentModel.DataAnnotations.Schema;
using Framework.Domain;

namespace HomeCleaning.Domain
{
    public class Order : Entity
    {
        public Customer Customer { get; set; }
        
        public int SpaceSizeId { get; set; }

        [ForeignKey("SpaceSizeId")]
       public SpaceSize SpaceSize { get; set; }
      
       public int CleaningPackageId { get; set; }
      
       [ForeignKey("CleaningPackageId")]
       public CleaningPackage CleaningPackage { get; set; }
      
        public int CleaningExtraOptionId { get; set; }

        [ForeignKey("CleaningExtraOptionId")]
        public CleaningExtraOption CleaningExtraOption { get; set; }

        public Address Address { get; set; }

        public DateTime ScheduledTime { get; set; }

        public DateTime RegisterTime { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal Tax { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }
}