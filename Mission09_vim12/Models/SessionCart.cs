using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Mission09_vim12.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mission09_vim12.Models
{
    //Inherit from Cart class
    public class SessionCart : Cart
    {
        public static Cart GetCart (IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }
        [JsonIgnore]
        public ISession Session { get; set; }
        //Override methods in parent class
        public override void AddItem(Book boo, int qty)
        {
            base.AddItem(boo, qty);
            Session.SetJson("Cart", this);
        }
        public override void RemoveItem(Book boo)
        {
            base.RemoveItem(boo);
            Session.SetJson("Cart", this);
        }
        public override void ClearCart()
        {
            base.ClearCart();
            Session.Remove("Cart");
        }
    }
}
