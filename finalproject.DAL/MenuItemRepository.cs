using finproject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finproject.DAL
{
    public class MenuItemRepository
    {
        private readonly AppDbContext dbContext;

        public MenuItemRepository()
        {
            dbContext = new AppDbContext();
        }

        public MenuItem GetById(int id)
        {
            return dbContext.MenuItems.FirstOrDefault(m => m.Id == id);
        }

        public List<MenuItem> GetAll()
        {
            return dbContext.MenuItems.ToList();
        }

        public void Add(MenuItem item)
        {
            dbContext.MenuItems.Add(item);
            dbContext.SaveChanges();
        }

        public void Update(MenuItem item)
        {
            dbContext.MenuItems.Update(item);
            dbContext.SaveChanges();
        }

        public void Delete(MenuItem item)
        {
            dbContext.MenuItems.Remove(item);
            dbContext.SaveChanges();
        }
    }
}
