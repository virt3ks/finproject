using finproject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finproject.DAL
{
    public class EmployeeRepository
    {
        private readonly AppDbContext dbContext;

        public EmployeeRepository()
        {
            dbContext = new AppDbContext();
        }

        public Employee GetById(int id)
        {
            return dbContext.Employees.FirstOrDefault(e => e.Id == id);
        }

        public List<Employee> GetAll()
        {
            return dbContext.Employees.ToList();
        }

        public void Add(Employee emp)
        {
            dbContext.Employees.Add(emp);
            dbContext.SaveChanges();
        }

        public void Update(Employee emp)
        {
            dbContext.Employees.Update(emp);
            dbContext.SaveChanges();
        }

        public void Delete(Employee emp)
        {
            dbContext.Employees.Remove(emp);
            dbContext.SaveChanges();
        }
    }
}