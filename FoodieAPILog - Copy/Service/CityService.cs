using HungryHUB.Database;
using HungryHUB.Entity;
using HungryHUB.Service;

namespace HungryHUB.Services
{
    public class CityService : ICityService
    {
        private readonly MyContext _context;

        public CityService(MyContext context)
        {
            _context = context;
        }

        public void CreateCity(City city)
        {
            try
            {
                _context.Cities.Add(city);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteCity(int CityID)
        {
            City city = _context.Cities.Find(CityID);
            if (city != null)
            {
                try
                {
                    _context.Cities.Remove(city);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public void EditCity(City city)
        {
            try
            {
                _context.Cities.Update(city);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<City> GetAllCities()
        {
            try
            {
                return _context.Cities.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}