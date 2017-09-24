using System.Collections.Generic;

namespace ShopHierarchy5.Models
{
   public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public ICollection<ItemsOrders> Items { get; set; } = new List<ItemsOrders>();
    }
}
