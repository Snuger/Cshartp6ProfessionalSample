using Microsoft.AspNetCore.Mvc.Rendering;
using MvcApplicationSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApplicationSample.Extensions
{
    public static  class SelectListItemExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItem(this IEnumerable<Zoning> zonings, int selectedId) {
            return zonings.Select(item =>
            new SelectListItem
            {
                Selected=item.Code==selectedId,
                Text=item.Name,
                Value=item.Code.ToString()
            });
        }
    }
}
