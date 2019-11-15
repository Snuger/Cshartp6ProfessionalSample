using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageSample.Data;

namespace RazorPageSample.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly RazorDbContext dbContext;
        private readonly IEasyCachingProvider _cachingProvider;

        private readonly IEasyCachingProviderFactory _easyCachingProviderFactory;

        public CreateModel(RazorDbContext dbContext, IEasyCachingProviderFactory easyCachingProviderFactory)
        {
            this.dbContext = dbContext;
            _easyCachingProviderFactory = easyCachingProviderFactory;
            _cachingProvider = _easyCachingProviderFactory.GetCachingProvider("DefaultRedis");
        }

        [BindProperty]
        public Product Product { get; set; }


        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid)
                return Page();
            dbContext.Products.Add(Product);
            int result= await dbContext.SaveChangesAsync();
            if (result > 0)
            {
                var productList = await _cachingProvider.GetAsync<List<Product>>("PRODUCT_LIST");
                if (!productList.IsNull)
                {                    
                    productList.Value.Add(Product);
                    //await _cachingProvider.RemoveAsync("PRODUCT_LIST");
                    await _cachingProvider.SetAsync("PRODUCT_LIST",(List<Product>) productList.Value, new TimeSpan(0, 5, 0));
                }
              
            }          
            return RedirectToPage("./index");
        }

        public IActionResult OnGet()
        {
            return Page();

        }
    }
}