using AutoMapper;
using HungryHUB.DTO;
using HungryHUB.Entity;

namespace HungryHUB.Profiles
{
    public class CouponProfile : Profile
    {
        public CouponProfile()
        {
            CreateMap<Coupon, CouponDTO>();
            CreateMap<CouponDTO, Coupon>();
        }
    }
}
