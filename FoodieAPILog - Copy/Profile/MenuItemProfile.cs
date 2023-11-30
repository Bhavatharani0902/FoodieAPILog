using AutoMapper;
using HungryHUB.DTO;
using HungryHUB.Entity;

namespace HungryHUB.Profiles
{
    public class MenuItemProfile : Profile
    {
        public MenuItemProfile()
        {
            CreateMap<MenuItem, MenuItemDTO>();
            CreateMap<MenuItemDTO, MenuItem>();
        }
    }
}
