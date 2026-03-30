using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proekt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proekt.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Client> _userManager;

        public CartController(ApplicationDbContext context, UserManager<Client> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);

            var cart = _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.ClientId == userId);

            return View(cart);
        }


        public async Task<IActionResult> AddToCart(int id)
        {
            var userId = _userManager.GetUserId(User);

           
            var cart = _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefault(c => c.ClientId == userId);

            
            if (cart == null)
            {
                cart = new Cart
                {
                    ClientId = userId,
                    CreatedDate = DateTime.Now,
                    CartItems = new List<CartItem>()
                };

                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

          
            var existingItem = cart.CartItems
                .FirstOrDefault(ci => ci.ProductId == id);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                var newItem = new CartItem
                {
                    ProductId = id,
                    Quantity = 1,
                    DateAdded = DateTime.Now,
                    CartId = cart.Id
                };

                _context.CartItem.Add(newItem);
            }

            await _context.SaveChangesAsync();

           return RedirectToAction("Index","Products");
        }


        public async Task<IActionResult> Remove(int id)
        {
            var item = await _context.CartItem.FindAsync(id);

            if (item != null)
            {
                _context.CartItem.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

       
        public async Task<IActionResult> Increase(int id)
        {
            var item = await _context.CartItem.FindAsync(id);

            if (item != null)
            {
                item.Quantity++;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

       
        public async Task<IActionResult> Decrease(int id)
        {
            var item = await _context.CartItem.FindAsync(id);

            if (item != null)
            {
                item.Quantity--;

                if (item.Quantity <= 0)
                {
                    _context.CartItem.Remove(item);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
