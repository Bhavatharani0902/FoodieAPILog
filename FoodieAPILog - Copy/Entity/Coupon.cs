using System;

namespace HungryHUB.Entity
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string Code { get; set; }
        public double DiscountAmount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
    }
}
