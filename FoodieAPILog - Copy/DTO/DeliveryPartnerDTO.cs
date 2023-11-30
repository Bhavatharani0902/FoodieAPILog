namespace HungryHUB.DTO
{
    public class DeliveryPartnerDTO
    {
        public int? DeliveryPartnerId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAvailable { get; set; }
        public int CityId { get; set; }
    }
}
