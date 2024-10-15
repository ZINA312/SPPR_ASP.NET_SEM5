using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.Domain.Models;
using WEB_253503_Kudosh.UI.Services.TelescopeProductService;
using WEB_253503_Kudosh.UI.Services.TelescopeCategoryService;

namespace WEB_253503_Kudosh.UI.Areas.AdminArea.Views.Admin
{
    public class CreateModel : PageModel
    {
        private readonly ITelescopeService _telescopeService;
        private readonly ICategoryService _categoryService; 

        public CreateModel(ITelescopeService telescopeService, ICategoryService categoryService)
        {
            _telescopeService = telescopeService;
            _categoryService = categoryService; 
        }

        [BindProperty]
        public TelescopeEntity TelescopeEntity { get; set; } = default!;
        [BindProperty]
        public IFormFile? Image {  get; set; }
        public List<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>(); 

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await _categoryService.GetCategoryListAsync();
            if (response.Successfull)
            {
                Categories = response.Data;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to load categories.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            try
            {
                await _telescopeService.CreateProductAsync(TelescopeEntity, Image);
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