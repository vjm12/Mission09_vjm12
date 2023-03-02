using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_vim12.Models
{
    //Implementation of interface
    public class EFBookstoreRepository : IBookstoreRepository
    {
        //Set up context
        private BookstoreContext context { get; set; }
        public EFBookstoreRepository(BookstoreContext temp)
        {
            context = temp;
        }
        public IQueryable<Book> Books => context.Books;
    }
}
