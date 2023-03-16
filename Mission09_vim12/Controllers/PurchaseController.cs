using Microsoft.AspNetCore.Mvc;
using Mission09_vim12.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_vim12.Controllers
{
    public class PurchaseController : Controller
    {
        private IPurchaseRepository repo { get; set; }
        private Cart cart { get; set; }
        //constructor to pass databsae info
        public PurchaseController(IPurchaseRepository temp, Cart c)
        {
            repo = temp;
            cart = c;
        }
        //Info for purchase/checkout
        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Purchase());
        }
        [HttpPost]
        public IActionResult Checkout(Purchase purchase)
        {
            if (cart.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");

            }
            if (ModelState.IsValid)
            {
                purchase.Lines = cart.Items.ToArray();
                repo.SavePurchase(purchase);
                cart.ClearCart();
                return RedirectToPage("/PurchaseCompleted");
            }
            else
            {
                return View();
            }

        }

    }
}
