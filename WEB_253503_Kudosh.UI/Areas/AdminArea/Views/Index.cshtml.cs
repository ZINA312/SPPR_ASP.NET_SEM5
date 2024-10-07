using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.UI.Services.TelescopeProductService;

namespace WEB_253503_Kudosh.UI.Areas.AdminArea.Views
{
    public class IndexModel : PageModel
    {
        private readonly ITelescopeService _telescopeService;

        public IndexModel(ITelescopeService telescopeService)
        {
            _telescopeService = telescopeService;
        }

        public IList<TelescopeEntity> TelescopeEntity { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var response = await _telescopeService.GetProductListAsync(null, 1);
            TelescopeEntity = response.Data.Items;
        }
    }
}