using HungryHUB.Entity;
using HungryHUB.Service;
using System.Collections.Generic;

namespace HungryHUB.Service
{
    public interface IRestaurantService
    {
        List<Restaurant> GetAllRestaurants();
        Restaurant GetRestaurantById(int restaurantId);
        void CreateRestaurant(Restaurant restaurant);
        void UpdateRestaurant(int restaurantId, Restaurant updatedRestaurant);
        void DeleteRestaurant(int restaurantId);
        List<Restaurant> GetRestaurantsByCity(string cityName);
        Restaurant GetRestaurantByName(string name);
        void ActivateRestaurant(int restaurantId);
        void DeactivateRestaurant(int restaurantId);
    }
}
