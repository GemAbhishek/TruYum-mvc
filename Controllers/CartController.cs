using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TruYum.Models;

namespace TruYum.Controllers
{
    public class CartController : Controller
    {
        private TruYumContext _context;
        public CartController()
        {
            _context = new TruYumContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Cart
        public ActionResult Index()
        {
            var cartItems = _context.Carts.Include(m => m.MenuItem).ToList();

            return View(cartItems);
        }

        public ActionResult Delete(int id)
        {
            var cItem = _context.Carts.Single(c => c.Id == id);
            _context.Carts.Remove(cItem);
            _context.SaveChanges();

            return RedirectToAction("Index", "Cart");
        }
    }
}