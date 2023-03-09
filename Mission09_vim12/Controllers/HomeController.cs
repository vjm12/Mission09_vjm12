using Microsoft.AspNetCore.Mvc;
using Mission09_vim12.Models;
using Mission09_vim12.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_vim12.Controllers
{
    public class HomeController : Controller
    {
        //create instance of repository
        private IBookstoreRepository repo;
        public HomeController (IBookstoreRepository temp)
        {
            repo = temp;
        }
        public IActionResult Index(string bookCategory, int pageNum = 1)
        {
            int pageSize = 10;
            //Pass all information to the view
            var x = new BooksViewModel
            {
                Books = repo.Books
                .Where(b=>b.Category==bookCategory || bookCategory ==null)
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumBooks = 
                    (bookCategory == null 
                    ? repo.Books.Count() 
                    : repo.Books.Where(x => x.Category == bookCategory).Count()),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };
        
            return View(x);
        }
    }
}
