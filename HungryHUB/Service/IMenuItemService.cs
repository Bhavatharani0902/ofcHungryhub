
using HungryHUB.Entity;
namespace HungryHUB.Service
{
    public interface IMenuItemService
    {
        List<MenuItem> GetAllMenuItems();
        MenuItem GetMenuItemById(int menuItemId);
        MenuItem GetMenuItemByName(string menuItemName);
        List<MenuItem> GetMenuItemsByRestaurantName(string restaurantName);
        void CreateMenuItem(MenuItem menuItem);
        void UpdateMenuItem(int menuItemId, MenuItem updatedMenuItem);
        void DeleteMenuItem(int menuItemId);
        IEnumerable<MenuItem> SearchMenuItems(string query);
    }
}
