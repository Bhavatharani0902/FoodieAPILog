using HungryHUB.Database;
using HungryHUB.Entity;

namespace HungryHUB.Service
{
    public class CouponService : ICouponService
    {
        private readonly MyContext _context;

        public CouponService(MyContext context)
        {
            _context = context;
        }

        public void CreateCoupon(Coupon coupon)
        {
            try
            {
                _context.Coupons.Add(coupon);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteCoupon(int couponId)
        {
            var coupon = _context.Coupons.Find(couponId);

            if (coupon != null)
            {
                _context.Coupons.Remove(coupon);
                _context.SaveChanges();
            }
        }

        public Coupon GetCouponById(int couponId)
        {
            return _context.Coupons.Find(couponId);
        }

        public List<Coupon> GetAllCoupons()
        {
            return _context.Coupons.ToList();
        }
    }
}
