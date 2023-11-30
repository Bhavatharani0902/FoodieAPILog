using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HungryHUB.Entity
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        public Restaurant Restaurant { get; set; }

        

        public int DeliveryPartnerId { get; set; }
     
        [ForeignKey(nameof(DeliveryPartnerId))]

        public DeliveryPartner DeliveryPartner { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

    }
}
