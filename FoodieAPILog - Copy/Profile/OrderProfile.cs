using AutoMapper;
using HungryHUB.DTO;
using HungryHUB.Entity;

namespace HungryHUB.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, Order>();
        }
    }
}
