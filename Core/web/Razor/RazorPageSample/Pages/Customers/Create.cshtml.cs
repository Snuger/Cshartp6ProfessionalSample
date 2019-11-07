using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageSample.Data;

namespace RazorPageSample.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly RazorDbContext _db;

        public CreateModel(RazorDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public RazorPageSample.Data.Customer Customer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _db.Customers.AddAsync(Customer);
            await _db.SaveChangesAsync();
            return RedirectToPage("/Index");

        }


    }
}
