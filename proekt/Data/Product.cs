namespace proekt.Data
{
    public class Product
    {
        public int Id { get; set; }
        public int CatalogNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public int DateAdded { get; set; }
        public int CategoryId { get; set; }
        public Category Categories { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
