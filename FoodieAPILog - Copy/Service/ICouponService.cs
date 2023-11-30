using System.Collections.Generic;
using HungryHUB.Entity;

namespace HungryHUB.Service
{
    public interface ICouponService
    {
        void CreateCoupon(Coupon coupon);
        void DeleteCoupon(int couponId);
        Coupon GetCouponById(int couponId);
        List<Coupon> GetAllCoupons();
    }
}
