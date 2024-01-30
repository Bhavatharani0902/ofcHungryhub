
using System.ComponentModel.DataAnnotations;

namespace HungryHUB.DTO
{
    public class CartItemDTO
        {
        public int CartId { get; set; }
         public int MenuItemId { get; set; }
        [Required]
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public int Quantity { get; set; }
        }
    }

