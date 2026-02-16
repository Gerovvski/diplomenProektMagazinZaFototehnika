namespace proekt.Data
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int DateAdded { get; set; }
        public int CartId {  get; set; }
        public Cart Carts { get; set; }
        public int ProductId { get; set; }
        public Product Products { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
