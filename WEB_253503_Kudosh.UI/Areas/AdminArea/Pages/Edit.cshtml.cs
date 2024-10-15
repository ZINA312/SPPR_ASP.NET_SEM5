using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.UI.Services.TelescopeProductService;
using System.Diagnostics;
using WEB_253503_Kudosh.UI.Services.TelescopeCategoryService;

namespace WEB_253503_Kudosh.UI.Areas.AdminArea.Views.Admin
{
    public class EditModel : PageModel
    {
        private readonly ITelescopeService _telescopeService;
        private readonly ICategoryService _categoryService;

        public EditModel(ITelescopeService telescopeService, ICategoryService categoryService)
        {
            _telescopeService = telescopeService;
            _categoryService = categoryService;
        }

        [BindProperty]
        public TelescopeEntity TelescopeEntity { get; set; } = default!;
        [BindProperty]
        public IFormFile? Image { get; set; }
        public List<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var response = await _categoryService.GetCategoryListAsync();
            if (response.Successfull)
            {
                Categories = response.Data;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to load categories.");
            }
            var telescopeResponse = await _telescopeService.GetProductByIdAsync(id);
            Debug.WriteLine("responce" + telescopeResponse);
            if (telescopeResponse == null || telescopeResponse.Data == null)
            {
                return NotFound();
            }

            TelescopeEntity = telescopeResponse.Data;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _telescopeService.UpdateProductAsync(TelescopeEntity.Id, TelescopeEntity, Image);
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