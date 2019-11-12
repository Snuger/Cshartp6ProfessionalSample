using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageSample.Data;

namespace RazorPageSample.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly RazorDbContext dbContext;

        public CreateModel(RazorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [BindProperty]
        public Product Product { get; set; }


        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid)
                return Page();
            dbContext.Products.Add(Product);
            await dbContext.SaveChangesAsync();
            return RedirectToPage("./index");
        }

        public IActionResult OnGet()
        {
            return Page();

        }
    }
}