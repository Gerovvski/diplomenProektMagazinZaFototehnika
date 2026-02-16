namespace proekt.Data
{
    public class Order
    {
        public int Id { get; set; }
        public int OrderDate { get; set; }
        public int TotalPrice { get; set; }
        public int OrderDetailId { get; set; }
        public OrderDetail OrderDetail { get; set; }
       
    }
}
