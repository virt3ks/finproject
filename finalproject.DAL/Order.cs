using finproject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finproject.DAL
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }

        public decimal TotalPrice { get; set; }

        public string PaymentMethod { get; set; }
    }
}
