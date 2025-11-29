using finproject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace finproject.DAL
{
    public class OrderRepository
    {
        private readonly AppDbContext dbContext;

        public OrderRepository()
        {
            dbContext = new AppDbContext();
        }

        public Order GetById(int id)
        {
            return dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.MenuItem)
                .FirstOrDefault(o => o.Id == id);
        }

        public List<Order> GetAll()
        {
            return dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.MenuItem)
                .ToList();
        }

        public void Add(Order order)
        {
            dbContext.Orders.Add(order);
            dbContext.SaveChanges();
        }

        public void Update(Order order)
        {
            dbContext.Orders.Update(order);
            dbContext.SaveChanges();
        }

        public void Delete(Order order)
        {
            dbContext.Orders.Remove(order);
            dbContext.SaveChanges();
        }
    }
}
