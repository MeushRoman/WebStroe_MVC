using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Adress { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int? StatusId { get; set; }   
        public OrderStatus OrderStatus { get; set; }
        public IEnumerable<OrderProducts> OrderProducts { get; set; }
    }
}
