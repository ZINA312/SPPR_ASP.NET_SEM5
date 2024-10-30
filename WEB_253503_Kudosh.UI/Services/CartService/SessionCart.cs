﻿using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.UI.Extensions;

namespace WEB_253503_Kudosh.UI.Services.CartService
{
    public class SessionCart : Cart
    {
        public ISession? Session { get; set; }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;
            SessionCart cart = session.Get<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        public override void AddToCart(TelescopeEntity telescope)
        {
            base.AddToCart(telescope);
            Session?.Set<Cart>("Cart", this);
        }
        public override void RemoveItems(int id)
        {
            base.RemoveItems(id);
            Session?.Set<Cart>("Cart", this);
        }
        public override void ClearAll()
        {
            base.ClearAll();
            Session?.Remove("Cart");
        }
    }
}
