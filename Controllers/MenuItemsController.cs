using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TruYum.Models;

namespace TruYum.Controllers
{
    public class MenuItemsController : Controller
    {
        private TruYumContext _context;
        public MenuItemsController()
        {
            _context = new TruYumContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //bool isAdmin = false;  //---default value is false---(***NOT WORKING***)
                                    //***this is bad approach not working*****

        // GET: MenuItems
        public ActionResult Index(bool isAdmin = false)
        {            
            ViewBag.isAdmin = isAdmin;
            var menuItems = _context.MenuItems.Include(m => m.Category).ToList();

            return View(menuItems);
        }
        public ActionResult AdminMenu()
        {
            //this.isAdmin = true; //***this is bad approach not working*****

            return RedirectToAction("Index", "MenuItems", new { isAdmin = true });
        }

        public ActionResult CustomerMenu()
        {            
            //-----below code no longer needed --> redirecting to index action-----
            //var menuItems = _context.MenuItems.Include(m => m.Category).Where(m => m.Active == true).ToList();

            return RedirectToAction("Index", "MenuItems");
        }

        public ActionResult Create()
        {
            var viewModel = new UpdateViewModel
            {
                MenuItem = new MenuItem(),
                Categories = _context.Categories.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MenuItem menuItem)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new UpdateViewModel
                {
                    MenuItem = menuItem,
                    Categories = _context.Categories.ToList()
                };

                return View("Create", viewModel);
            }

            if (menuItem.Id == 0)
            {
                _context.MenuItems.Add(menuItem);
            }
            else
            {
                var menuById = _context.MenuItems.Single(m => m.Id == menuItem.Id);
                menuById.Name = menuItem.Name;
                menuById.Price = menuItem.Price;
                menuById.FreeDelivery = menuItem.FreeDelivery;
                menuById.DateOfLaunch = menuItem.DateOfLaunch;
                menuById.Active = menuItem.Active;
                menuById.CategoryId = menuItem.CategoryId;

            }

            _context.SaveChanges();

            return RedirectToAction("Index", "MenuItems", new { isAdmin = true });

        }

        public ActionResult Edit(int id)
        {
            var menuItem = _context.MenuItems.SingleOrDefault(m => m.Id == id);

            if (menuItem == null)
                return HttpNotFound();

            var viewModel = new UpdateViewModel
            {
                MenuItem = menuItem,
                Categories = _context.Categories.ToList()
            };

            return View(viewModel);
        }

        
        public ActionResult AddToCart(int id)
        {
            var cartItem = new Cart();
            cartItem.UserId = 1;
            cartItem.MenuItemId = id;

            _context.Carts.Add(cartItem);
            _context.SaveChanges();

            return RedirectToAction("Index", "Cart");
        }

    }
}
 