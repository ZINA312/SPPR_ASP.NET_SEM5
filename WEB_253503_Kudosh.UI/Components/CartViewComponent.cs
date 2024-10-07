using Microsoft.AspNetCore.Mvc;

namespace WEB_253503_Kudosh.UI.Components
{
    public class CartViewComponent : ViewComponent
    {


        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
