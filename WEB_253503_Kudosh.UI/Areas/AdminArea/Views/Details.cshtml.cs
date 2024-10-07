using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.UI.Services.TelescopeProductService;

namespace WEB_253503_Kudosh.UI.Areas.AdminArea.Views
{
    public class DetailsModel : PageModel
    {
        private readonly ITelescopeService _telescopeService;

        public DetailsModel(ITelescopeService telescopeService)
        {
            _telescopeService = telescopeService;
        }

        public TelescopeEntity TelescopeEntity { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var telescopeResponse = await _telescopeService.GetProductByIdAsync(id);
            if (telescopeResponse == null || telescopeResponse.Data == null)
            {
                return NotFound();
            }

            TelescopeEntity = telescopeResponse.Data;
            return Page();
        }
    }
}