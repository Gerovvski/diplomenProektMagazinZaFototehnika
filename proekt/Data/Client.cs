using Microsoft.AspNetCore.Identity;

namespace proekt.Data
{
    public class Client:IdentityUser
    {
       
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public ICollection<Cart> Carts { get; set; }
     
        //public int CartId {  get; set; }
        //public Cart Cart { get; set; }
    }
}
