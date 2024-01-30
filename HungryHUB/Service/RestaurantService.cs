﻿using HungryHUB.Database;
using HungryHUB.Entity;
namespace HungryHUB.Service
{
    public class RestaurantService : IRestaurantService
    {
        private readonly MyContext _context;

        public RestaurantService(MyContext context)
        {
            _context = context;
        }

        public List<Restaurant> GetAllRestaurants()
        {
            return _context.Restaurants.ToList();
        }

        public Restaurant GetRestaurantById(int restaurantId)
        {
            return _context.Restaurants.Find(restaurantId);
        }

        public void CreateRestaurant(Restaurant restaurant)
        {
            try
            {
                _context.Restaurants.Add(restaurant);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateRestaurant(int restaurantId, Restaurant updatedRestaurant)
        {
            var existingRestaurant = _context.Restaurants.Find(restaurantId);

            if (existingRestaurant != null)
            {
                existingRestaurant.Name = updatedRestaurant.Name;
                existingRestaurant.CityID = updatedRestaurant.CityID;

                _context.Restaurants.Update(existingRestaurant);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"Restaurant with ID {restaurantId} not found.");
            }
        }

        public void DeleteRestaurant(int restaurantId)
        {
            var restaurant = _context.Restaurants.Find(restaurantId);

            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                _context.SaveChanges();
            }
        }

        public List<Restaurant> GetRestaurantsByCity(string cityName)
        {
            return _context.Restaurants.Where(r => r.City.CityName == cityName).ToList();
        }

        public Restaurant GetRestaurantByName(string name)
        {
            // Assuming your Restaurant entity has a property named 'Name'
            return _context.Restaurants.FirstOrDefault(r => r.Name == name);
        }

        public void ActivateRestaurant(int restaurantId)
        {
            var restaurant = _context.Restaurants.Find(restaurantId);

            if (restaurant != null)
            {
                restaurant.IsActive = true;

                _context.Restaurants.Update(restaurant);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"Restaurant with ID {restaurantId} not found.");
            }
        }

        public void DeactivateRestaurant(int restaurantId)
        {
            var restaurant = _context.Restaurants.Find(restaurantId);

            if (restaurant != null)
            {
                restaurant.IsActive = false;

                _context.Restaurants.Update(restaurant);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"Restaurant with ID {restaurantId} not found.");
            }
        }
    }
}

