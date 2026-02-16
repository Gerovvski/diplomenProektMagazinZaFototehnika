namespace proekt.Data
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
