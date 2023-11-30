namespace HungryHUB.DTO
{
    public class OrderDTO
    {

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public int? DeliveryPartnerId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
