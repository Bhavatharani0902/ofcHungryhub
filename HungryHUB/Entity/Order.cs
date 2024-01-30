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

        [Required]
        public double totalPrice { get; set; }
        
        [Required]
        public int quantity { get; set; }


        [ForeignKey(nameof(RestaurantId))]
        public Restaurant Restaurant { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int MenuItemId { get; set; }

        [ForeignKey(nameof(MenuItemId))]
        public MenuItem MenuItem { get; set; }


    }
}
