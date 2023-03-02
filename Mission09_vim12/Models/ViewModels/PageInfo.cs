using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_vim12.Models.ViewModels
{
    public class PageInfo
    {
        public int TotalNumBooks { get; set; }
        public int BooksPerPage { get; set; }
        public int CurrentPage { get; set; }
        //calculate number of needed pages
        public int TotalPages => (int) Math.Ceiling(((double) TotalNumBooks / BooksPerPage));
    }
}
