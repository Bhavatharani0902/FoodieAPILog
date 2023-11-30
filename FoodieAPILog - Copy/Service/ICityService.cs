using HungryHUB.Entity;

namespace HungryHUB.Service
{
    public interface ICityService
    {
        void CreateCity(City city);
        List<City> GetAllCities();
        void EditCity(City city);
        void DeleteCity(int CityID);
    }
}