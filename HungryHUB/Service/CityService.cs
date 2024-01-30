using HungryHUB.Database;
using HungryHUB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void UpdateCity(int cityId, City updatedCity)
        {
            var existingCity = _context.Cities.Find(cityId);

            if (existingCity != null)
            {
                existingCity.CityName = updatedCity.CityName;

                _context.SaveChanges();
            }
        }

        public void DeleteCity(int cityId)
        {
            var city = _context.Cities.Find(cityId);

            if (city != null)
            {
                _context.Cities.Remove(city);
                _context.SaveChanges();
            }
        }

        public City GetCityById(int cityId)
        {
            return _context.Cities.Find(cityId);
        }

        public List<City> GetAllCities()
        {
            return _context.Cities.ToList();
        }
    }
}
