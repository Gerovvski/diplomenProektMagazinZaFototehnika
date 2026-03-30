namespace proekt.Data
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string ClientId { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<OrderDetail> Details { get; set; }
    }
}
