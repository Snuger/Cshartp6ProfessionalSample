using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RazorPageSample.Data;
using Microsoft.EntityFrameworkCore;

namespace RazorPageSample.Pages.Produces
{
    public class IndexModel : PageModel
    {

        private readonly RazorDbContext _dbContext;

        public IndexModel(RazorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public List<Product> Product { get; set; }

        public async Task OnGetAsync()
        {
            Product = await _dbContext.Products.ToListAsync();
        }


    }
}
