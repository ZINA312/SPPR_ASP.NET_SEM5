using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.UI.Services.TelescopeProductService;

namespace WEB_253503_Kudosh.UI.Areas.AdminArea.Views
{
    public class CreateModel : PageModel
    {
        private readonly ITelescopeService _telescopeService;

        public CreateModel(ITelescopeService telescopeService)
        {
            _telescopeService = telescopeService;
        }

        [BindProperty]
        public TelescopeEntity TelescopeEntity { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _telescopeService.CreateProductAsync(TelescopeEntity, null);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error creating product: {ex.Message}");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}