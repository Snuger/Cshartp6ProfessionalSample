using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RazorPageSample.Data;
using Microsoft.EntityFrameworkCore;
using RazorPageSample.Model;
using System.Linq.Expressions;

namespace RazorPageSample.Pages.Produces
{
    public class IndexModel : PageModel
    {

        private readonly RazorDbContext _dbContext;

        public IndexModel(RazorDbContext dbContext)
        {
            _dbContext = dbContext;          
        } 

        public List<Product> Product { get; set; }

        [BindProperty(SupportsGet =true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            Expression<Func<Product, bool>> exp = cu => true;
            if (!string.IsNullOrEmpty(SearchString))
                exp = cu => cu.Name.Contains(SearchString);
            Product = await _dbContext.Products.Where(exp).ToListAsync();
        }


    }
}
