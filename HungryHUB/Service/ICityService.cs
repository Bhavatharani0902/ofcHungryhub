using HungryHUB.Entity;
using System.Collections.Generic;

namespace HungryHUB.Services
{
    public interface ICityService
    {
        void CreateCity(City city);
        void UpdateCity(int cityId, City updatedCity);
        void DeleteCity(int cityId);
        City GetCityById(int cityId);
        List<City> GetAllCities();
    }
}
