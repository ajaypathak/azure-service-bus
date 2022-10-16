using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicsAndSubscriptions.Model
{
    public class Order
    {
        public List<Product> Products { get; set; }
        public string State { get; set; }

        public decimal TotalPrice { get; set; }
        public Order()
        {
            Products = new List<Product>();
        }
    }
}
