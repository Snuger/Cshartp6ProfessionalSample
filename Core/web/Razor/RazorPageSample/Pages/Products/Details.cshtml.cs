using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageSample.Data;
using Microsoft.EntityFrameworkCore;

namespace RazorPageSample.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly RazorDbContext dbContext;

        public DetailsModel(RazorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [BindProperty]
        public Product Product { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();
            Product = await dbContext.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (Product == null)
                return NotFound();
            return Page();

        }
    }
}