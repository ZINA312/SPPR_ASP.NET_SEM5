using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.UI.Services.TelescopeProductService;
using WEB_253503_Kudosh.UI.Extensions;

namespace WEB_253503_Kudosh.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly ITelescopeService _telescopeService;
        private readonly Cart _cart;

        public CartController(ITelescopeService telescopeService, Cart cart)
        {
            _telescopeService = telescopeService;
            _cart = cart;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [Route("[controller]/add/{id:int}")]
        public async Task<ActionResult> Add(int id, string returnUrl)
        {
            var data = await _telescopeService.GetProductByIdAsync(id);
            if (data.Successfull)
            {
                _cart.AddToCart(data.Data);
            }
            return Redirect(returnUrl);
        }
    }
}
