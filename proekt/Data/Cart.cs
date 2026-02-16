namespace proekt.Data
{
    public class Cart
    {
        public int Id { get; set; }

        public Client Client { get; set; }
        public string ClientId { get; set; }
        public int CreatedDate { get; set; }
       
        public int ProductId { get; set; }
        public Product Product { get; set; }
        //public ICollection<CartItem> CartItems { get; set; }
    }
}
