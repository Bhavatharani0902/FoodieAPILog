
using HungryHUB.Entity;
namespace HungryHUB.Service
{
    public interface IMenuItemService
    {
        List<MenuItem> GetAllMenuItems();
        MenuItem GetMenuItemById(int menuItemId);
        List<MenuItem> GetMenuItemsByRestaurant(int restaurantId);
        void CreateMenuItem(MenuItem menuItem);
        void UpdateMenuItem(int menuItemId, MenuItem updatedMenuItem);
        void DeleteMenuItem(int menuItemId);
    }
}
