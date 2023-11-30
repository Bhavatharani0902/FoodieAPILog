namespace HungryHUB.DTO
{
    public class CouponDTO
    {
        public int CouponId { get; set; }
        public string Code { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
    }
}
