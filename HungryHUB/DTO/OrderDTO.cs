namespace HungryHUB.DTO
{
    public class OrderDTO
    {

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public int quantity { get; set; }
        public double totalPrice { get; set; }
        public int? MenuItemId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
