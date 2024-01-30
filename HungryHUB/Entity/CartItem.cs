using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HungryHUB.Entity
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        [Required]
        public int MenuItemId { get; set; }

        [ForeignKey(nameof(MenuItemId))]
        public MenuItem MenuItem { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public int RestaurantId { get; set; }
        [ForeignKey(nameof(RestaurantId))]
        
        public Restaurant Restaurant { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
