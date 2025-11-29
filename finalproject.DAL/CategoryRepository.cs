using finproject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finproject.DAL
{
    public class CategoryRepository
    {
        private readonly AppDbContext dbContext;

        public CategoryRepository()
        {
            dbContext = new AppDbContext();
        }

        public Category GetById(int id)
        {
            return dbContext.Categories.FirstOrDefault(c => c.Id == id);
        }

        public List<Category> GetAll()
        {
            return dbContext.Categories.ToList();
        }

        public void Add(Category category)
        {
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
        }

        public void Update(Category category)
        {
            dbContext.Categories.Update(category);
            dbContext.SaveChanges();
        }

        public void Delete(Category category)
        {
            dbContext.Categories.Remove(category);
            dbContext.SaveChanges();
        }
    }
}
