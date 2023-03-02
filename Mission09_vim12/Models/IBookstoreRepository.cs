using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_vim12.Models
{
    //Pattern/template for class
    public interface IBookstoreRepository
    {
        IQueryable<Book> Books { get; }
    }
}
