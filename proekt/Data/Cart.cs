namespace proekt.Data
{
    public class Cart
    {
        public int Id { get; set; }

        public Client Client { get; set; }
        public string ClientId { get; set; }
        public DateTime CreatedDate { get; set; }


        public ICollection<CartItem> CartItems { get; set; }
    }
}
