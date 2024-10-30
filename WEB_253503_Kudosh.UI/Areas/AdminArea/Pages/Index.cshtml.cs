using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.DependencyResolver;
using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.UI.Services.TelescopeProductService;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WEB_253503_Kudosh.UI.Areas.AdminArea.Views.Admin
{
    public class IndexModel : PageModel
    {
        private readonly ITelescopeService _telescopeService;

        public IndexModel(ITelescopeService telescopeService)
        {
            _telescopeService = telescopeService;
            TelescopeEntities = new List<TelescopeEntity>();
        }
        [BindProperty]
        public List<TelescopeEntity> TelescopeEntities { get; set; }
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; }
        [BindProperty]
        public int TotalPages {  get; set; }
        public async Task OnGetAsync(int pageNo = 1)
        {
            var response = await _telescopeService.GetProductListAsync(null, pageNo);
            if (response?.Data?.Items != null)
            {
                TelescopeEntities = response.Data.Items;
                CurrentPage = response.Data.CurrentPage; 
                TotalPages = response.Data.TotalPages; 
            }
        }
    }
}