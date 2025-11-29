using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finproject.DAL
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Taste { get; set; }
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}
