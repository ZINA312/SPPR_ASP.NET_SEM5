using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.UI.Services.TelescopeProductService;

namespace WEB_253503_Kudosh.UI.Areas.AdminArea.Views
{
    public class EditModel : PageModel
    {
        private readonly ITelescopeService _telescopeService;

        public EditModel(ITelescopeService telescopeService)
        {
            _telescopeService = telescopeService;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(IFormFile? formFile)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _telescopeService.UpdateProductAsync(TelescopeEntity.Id, TelescopeEntity, formFile);
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError(string.Empty, $"Error updating product: {ex.Message}");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}