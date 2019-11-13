using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPageSample.Data;

namespace RazorPageSample.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly RazorDbContext dbContext;

        public DeleteModel(RazorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [BindProperty]
        public Product Product { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();
            Product = await dbContext.Products.FirstOrDefaultAsync(c=>c.Id==id);
            if (Product == null)
                return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id) {
            if (id == null)
                return NotFound();
            try
            {
                var product= await dbContext.Products.FindAsync(id);
                if (product != null)
                    dbContext.Products.Remove(product);
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Page();               
            }
            return RedirectToPage("./Index");
        }
    }
}