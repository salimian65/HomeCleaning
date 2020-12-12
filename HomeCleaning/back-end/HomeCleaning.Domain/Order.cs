using System;
using System.ComponentModel.DataAnnotations.Schema;
using Framework.Domain;

namespace HomeCleaning.Domain
{
    public class Order : Entity
    {
        public string ClientUserId { get; set; }

        [ForeignKey("ClientUserId")]
        public ApplicationUser ClientUser { get; set; }

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

    public class ServerRequest : Entity
    {
        public string ServerUserId { get; set; }

        [ForeignKey("ServerUserId")]
        public ApplicationUser ServerUser { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public ServerRequestStatus ServerRequestStatus { get; set; }

    }

    public enum ServerRequestStatus
    {
        ServerRequested = 1,
        Approved = 2,
        Rejected = 3,
        InProcessed = 3,
        Ended = 4
    }
}