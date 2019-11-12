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
    public class EditModel : PageModel
    {

        private readonly RazorDbContext dbContext;

        public EditModel(RazorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id==null)
                return NotFound();
            Product = await dbContext.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (Product == null)
                return NotFound();
            return Page();

        }

        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid)
                return Page();
            dbContext.Attach(Product).State = EntityState.Modified;
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                bool has= await   dbContext.Products.AnyAsync(e => e.Id == Product.Id);
                if (!has)
                    return Page();
                throw;
            }
            return RedirectToPage("./index");
        }
    }
}