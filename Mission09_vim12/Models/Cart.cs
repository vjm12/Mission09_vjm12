using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_vim12.Models
{
    public class Cart
    {
        public List<CartLineItem> Items { get; set; } = new List<CartLineItem>();

        public virtual void AddItem (Book boo, int qty)
        {
            CartLineItem line = Items
                .Where(b => b.Book.BookId == boo.BookId)
                .FirstOrDefault();
            if (line == null)
            {
                Items.Add(new CartLineItem
                {
                    Book = boo,
                    Quantity = qty
                });
            }
            else
            {
                line.Quantity += qty;
            }
        }
        //Create remove item method
        public virtual void RemoveItem(Book boo)
        {
            Items.RemoveAll(x => x.Book.BookId == boo.BookId);

        }
        //Create clear cart method
        public virtual void ClearCart()
        {
            Items.Clear();
        }
        public double CalculateTotal()
        {
            double sum = Items.Sum(x => x.Quantity * x.Book.Price);
            return sum;
        }

    }
    public class CartLineItem
    {
        [Key]
        public int LineID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
