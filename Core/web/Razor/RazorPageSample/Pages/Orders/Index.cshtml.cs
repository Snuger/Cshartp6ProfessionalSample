using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPageSample.Data;

namespace RazorPageSample.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly RazorPageSample.Data.RazorDbContext _context;

        public IndexModel(RazorPageSample.Data.RazorDbContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; }

        public async Task OnGetAsync()
        {
            Order = await _context.Orders.ToListAsync();
        }
    }
}
