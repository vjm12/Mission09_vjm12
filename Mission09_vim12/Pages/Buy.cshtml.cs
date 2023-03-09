using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mission09_vim12.Infrastructure;
using Mission09_vim12.Models;

namespace Mission09_vim12.Pages
{
    public class BuyModel : PageModel
    {
        //build instance of database
        private IBookstoreRepository repo { get; set; }
        public BuyModel(IBookstoreRepository temp)
        {
            repo = temp;
        }
        public Cart cart { get; set; }
        //get the url so the user can return to the page they were on before the cart
        public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }
        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);
            //initialize cart
            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            cart.AddItem(b, 1);

            //Set information from cart using the session
            HttpContext.Session.SetJson("cart", cart);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
