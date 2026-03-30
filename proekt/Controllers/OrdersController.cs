using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proekt.Data;

namespace proekt.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Client> _userManager;

        //метод за цхецкоут бутон
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout()
        {
     
            var userId = _userManager.GetUserId(User);

            
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.ClientId == userId);

           
           
            var order = new Order
            {
                ClientId = userId,
                OrderDate = DateTime.Now,
                TotalPrice = cart.CartItems.Sum(ci => (int)(ci.Product.Price * ci.Quantity)),
                Details = cart.CartItems.Select(ci => new OrderDetail
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    Price = (decimal)ci.Product.Price
                }).ToList()
            };

          
            _context.Orders.Add(order);

            _context.CartItem.RemoveRange(cart.CartItems);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

           
            return RedirectToAction("Index", "Orders", new { id = order.Id });
        }




        //detaili za ordr
        public IActionResult Details(int id)
        {
            var order = _context.Orders
                .Include(o => o.Details)
                .ThenInclude(or => or.Product)
                .FirstOrDefault(o => o.Id == id);
            return View(order);
        }







        public OrdersController(ApplicationDbContext context,UserManager<Client> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

        // GET: Orders/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Orders
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderDate,TotalPrice")] Order order)
        {
            order.OrderDate = DateTime.Now;
            order.ClientId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View(order);
        }

       

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }





            
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Details)
                .FirstOrDefaultAsync(o => o.Id == id);


            if (order != null)
            {
                _context.OrderDetails.RemoveRange(order.Details);
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
