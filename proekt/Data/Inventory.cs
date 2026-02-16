namespace proekt.Data
{
    public class Inventory
    {
        public int Id { get; set; }
        public string CurrentStock { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
