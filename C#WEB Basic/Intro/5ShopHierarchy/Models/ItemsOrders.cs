namespace ShopHierarchy5.Models
{
    public class ItemsOrders
    {
        public int ItemId { get; set; }

        public Item Item { get; set; }

        public int OrederId { get; set; }

        public Order Order { get; set; }
    }
}
