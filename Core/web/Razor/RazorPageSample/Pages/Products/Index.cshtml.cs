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
using EasyCaching.Core;

namespace RazorPageSample.Pages.Produces
{
    public class IndexModel : PageModel
    {

        private readonly RazorDbContext _dbContext;

        private readonly IEasyCachingProvider _cachingProvider;

        private readonly IEasyCachingProviderFactory _easyCachingProviderFactory;    

        public IndexModel(RazorDbContext dbContext, IEasyCachingProviderFactory easyCachingProviderFactory)
        {
            _dbContext = dbContext;
            _easyCachingProviderFactory = easyCachingProviderFactory;
            _cachingProvider = _easyCachingProviderFactory.GetCachingProvider("DefaultRedis");
        }
      
        public List<Product> Product { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var productList = await _cachingProvider.GetAsync<List<Product>>("PRODUCT_LIST");
            if (productList.IsNull)
            {
                var _list = await _dbContext.Products.ToListAsync();
                if (_list.Any()) {
                    await _cachingProvider.SetAsync("PRODUCT_LIST", _list, new TimeSpan(0, 1, 0));
                }
                Product = SearchFilter(SearchString,_list);
            }
            else
            {
                Product =SearchFilter(SearchString,(List<Product>)productList.Value);
            }

        }

        private Func<string, List<Product>, List<Product>> SearchFilter = (string search, List<Product> products) =>
        {           
            if (!string.IsNullOrEmpty(search))
                return products.Where(c => c.Name.Contains(search)).ToList();
            return products;
        };

    }
}
