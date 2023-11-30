using AutoMapper;
using HungryHUB.DTO;
using HungryHUB.Entity;

namespace HungryHUB.Profiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, RestaurantDTO>();
            CreateMap<RestaurantDTO, Restaurant>();
        }
    }
}
