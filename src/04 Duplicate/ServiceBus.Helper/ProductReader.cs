using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceBus.Helper
{
    public class ProductReader
    {
        List<Product> products;
        public ProductReader()
        {
            products = new List<Product>();

        }

        public List<Product> GetProducts()
        {
            products.Add(new Product { ProductName = "Head First Design Patterns: Building Extensible and Maintainable Object-Oriented Software, Second Edition (Grayscale Indian Edition)", Price = 1625, });
            products.Add(new Product { ProductName = "Design Patterns: Elements of Reusable Object-Oriented Software", Price = 653, });
            products.Add(new Product { ProductName = "Design Patterns", Price = 691, });
            products.Add(new Product { ProductName = "Design Patterns for Cloud Native Applications: Patterns in Practice Using APIs, Data, Events, and Streams (Grayscale Indian Edition)", Price = 1200, });
            products.Add(new Product { ProductName = "New Composite Mathematics Class 3", Price = 374, });
            products.Add(new Product { ProductName = "Rachnatmak Vyakaran | Hindi Grammar Book for Class 1 | Second Edition | By Pearson", Price = 205, });
            products.Add(new Product { ProductName = "Rachnatmak Vyakaran | Hindi Grammar Book for Class 2 | Second Edition | By Pearson", Price = 218, });
            products.Add(new Product { ProductName = "Rachnatmak Vyakaran | Hindi Grammar Book for Class 3 | Second Edition | By Pearson", Price = 257, });
            products.Add(new Product { ProductName = "Rachnatmak Vyakaran | Hindi Grammar Book for Class 4 | Second Edition | By Pearson", Price = 278, });
            products.Add(new Product { ProductName = "Number Line (Maths) | ICSE Class Third | Revised First Edition as per latest CISCE curriculum | By Pearson", Price = 307, });
            products.Add(new Product { ProductName = "CISCE Universal Science | For ICSE Class 3", Price = 385, });
            products.Add(new Product { ProductName = "My Watchtower: Social Studies | ICSE Class Third | First Edition | By Pearson", Price = 276, });
            products.Add(new Product { ProductName = "New Aster Advanced | English Coursebook| ICSE | Class 3", Price = 300, });
            products.Add(new Product { ProductName = "English Grammar & composition for Class 3 |Climb with Cornerstone", Price = 230, });
            products.Add(new Product { ProductName = "MTG Science (NSO) Olympiad Previous Years Papers with Mock Test Paper - Class 3", Price = 243, });
            File.WriteAllTextAsync(@"C:\temp\products.json", JsonSerializer.Serialize(products));
            return products;
        }
    }
}
