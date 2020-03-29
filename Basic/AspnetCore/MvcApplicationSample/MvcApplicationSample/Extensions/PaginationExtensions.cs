using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApplicationSample.Extensions
{
    [HtmlTargetElement("cus-pagination",Attributes = ItemsAttributeName)]
    public class PaginationExtensions:TagHelper
    {
        private const string ItemsAttributeName = "page-for";

        public PaginationExtensions(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        protected IHtmlGenerator Generator { get; }

        [HtmlAttributeName(ItemsAttributeName)]
        public IEnumerable<object> Items { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
        }
    }
}
