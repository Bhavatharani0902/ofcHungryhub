using HungryHUB.Database;
using HungryHUB.Entity;

namespace HungryHUB.Service
{
    public class MenuItemService : IMenuItemService
    {
        private readonly MyContext _context;

        public MenuItemService(MyContext context)
        {
            _context = context;
        }

        public List<MenuItem> GetAllMenuItems()
        {
            return _context.MenuItem.ToList();
        }

        public MenuItem GetMenuItemById(int menuItemId)
        {
            return _context.MenuItem.Find(menuItemId);
        }

        public List<MenuItem> GetMenuItemsByRestaurant(int restaurantId)
        {
            return _context.MenuItem.Where(item => item.RestaurantId == restaurantId).ToList();
        }
        public MenuItem GetMenuItemByName(string menuItemName)
        {
            return _context.MenuItem.FirstOrDefault(m => m.Name == menuItemName);
        }

        public void CreateMenuItem(MenuItem menuItem)
        {
            try
            {
                _context.MenuItem.Add(menuItem);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateMenuItem(int menuItemId, MenuItem updatedMenuItem)
        {
            var existingMenuItem = _context.MenuItem.Find(menuItemId);

            if (existingMenuItem != null)
            {
                existingMenuItem.Name = updatedMenuItem.Name;
                existingMenuItem.Description = updatedMenuItem.Description;
                existingMenuItem.Price = updatedMenuItem.Price;
                existingMenuItem.RestaurantId = updatedMenuItem.RestaurantId;

                _context.MenuItem.Update(existingMenuItem);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"MenuItem with ID {menuItemId} not found.");
            }
        }

        public void DeleteMenuItem(int menuItemId)
        {
            var menuItem = _context.MenuItem.Find(menuItemId);

            if (menuItem != null)
            {
                _context.MenuItem.Remove(menuItem);
                _context.SaveChanges();
            }
        }
        public IEnumerable<MenuItem> SearchMenuItems(string query)
        {
            // Add logic to search for menu items based on the query
            // You might want to search by item name, description, or other relevant fields
            return _context.MenuItem.Where(item => item.Name.Contains(query));
        }

        public List<MenuItem> GetMenuItemsByRestaurantName(string restaurantName)
        {
            var menuItems = _context.MenuItem
        .Where(item => item.Restaurant.Name == restaurantName)
        .ToList();

            return menuItems;
        }
    }
}
