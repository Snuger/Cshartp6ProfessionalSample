using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcApplicationSample.Models;

namespace MvcApplicationSample.ViewComponents
{
    public class ZoningPickerViewComponent: ViewComponent
    {
        private readonly ZoningContetxt contetxt;

        public ZoningPickerViewComponent(ZoningContetxt contetxt)
        {
            this.contetxt = contetxt;
        }

        public Task<IViewComponentResult> InvokeAsync(int root = 0) =>
            Task.FromResult<IViewComponentResult>(View(GetRoot(root)));

        public IEnumerable<Zoning> GetRoot(int rootCode) =>
            contetxt.Zonings.Where(o => o.ParentCode == rootCode);

    }
}
