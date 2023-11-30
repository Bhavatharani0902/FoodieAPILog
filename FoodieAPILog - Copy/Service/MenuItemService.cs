
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
    }
}
