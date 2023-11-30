using AutoMapper;
using HungryHUB.DTO;
using HungryHUB.Entity;

namespace HungryHUB.Profiles
{
    public class DeliveryPartnerProfile : Profile
    {
        public DeliveryPartnerProfile()
        {
            CreateMap<DeliveryPartner, DeliveryPartnerDTO>();
            CreateMap<DeliveryPartnerDTO, DeliveryPartner>();
        }
    }
}
