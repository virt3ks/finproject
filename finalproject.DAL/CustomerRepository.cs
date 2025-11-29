using finproject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finproject.DAL
{
    public class CustomerRepository
    {
        private readonly AppDbContext dbContext;

        public CustomerRepository()
        {
            dbContext = new AppDbContext();
        }

        public Customer GetById(int id)
        {
            return dbContext.Customers.FirstOrDefault(c => c.Id == id);
        }

        public List<Customer> GetAll()
        {
            return dbContext.Customers.ToList();
        }

        public void Add(Customer customer)
        {
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();
        }

        public void Update(Customer customer)
        {
            dbContext.Customers.Update(customer);
            dbContext.SaveChanges();
        }

        public void Delete(Customer customer)
        {
            dbContext.Customers.Remove(customer);
            dbContext.SaveChanges();
        }
    }
}
