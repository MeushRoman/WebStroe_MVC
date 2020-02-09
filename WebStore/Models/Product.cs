using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Discription { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public int? StoreId { get; set; }
        public Store Store { get; set; }
        public IEnumerable<OrderProducts> OrderProducts { get; set; }
    }
}
