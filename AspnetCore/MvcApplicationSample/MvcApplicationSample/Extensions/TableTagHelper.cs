using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace MvcApplicationSample.Extensions
{
    [
        HtmlTargetElement("table", Attributes = ItemsAttributeName),
        HtmlTargetElement("table", Attributes = PageSizeAttribute), 
        HtmlTargetElement("table", Attributes = PageShowAttribute),
        HtmlTargetElement("table", Attributes = ActionAttribute),
        HtmlTargetElement("table", Attributes = PageNumberAttribute)]
    public class TableTagHelper:TagHelper
    {
        private const string ItemsAttributeName = "table-for";
        private const string PageSizeAttribute = "table-pagesize";
        private const string PageShowAttribute = "table-pageshow";
        private const string ActionAttribute = "table-action";
        private const string PageNumberAttribute = "table-pagenumber";


        protected IHtmlGenerator Generator { get; }

        public TableTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }       

        [HtmlAttributeName(ItemsAttributeName)]
        public IEnumerable<object> Items { get; set; }       

        [HtmlAttributeName(PageSizeAttribute)]
        public int PageSize { get; set; }

        [HtmlAttributeName(PageShowAttribute)]
        public int PageShowNums { get; set; } = 5;

        [HtmlAttributeName(ActionAttribute)]
        public string ActionUrl { get; set; }

        [HtmlAttributeName(PageNumberAttribute)]
        public int PageNumber { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("row");

            TagBuilder table = new TagBuilder("table");
            var attributes = context.AllAttributes.Where(at=>at.Name!=ItemsAttributeName).ToDictionary(at=>at.Name);
            table.GenerateId(context.UniqueId,"id");
            table.MergeAttributes(attributes);

            var tr = new TagBuilder("tr");
            var heading = Items.First();
            PropertyInfo[] properties = heading.GetType().GetProperties();
            foreach (var prop in properties) {
                var th = new TagBuilder("th");
                var  attr = prop.GetCustomAttribute<DisplayNameAttribute>(true);                   
                th.InnerHtml.AppendHtml(attr == null?prop.Name: attr.DisplayName);
                tr.InnerHtml.AppendHtml(th);
            }
            table.InnerHtml.AppendHtml(tr);
            foreach (var item in Items) {
                tr = new TagBuilder("tr");
                foreach (var prope in properties) {
                    var td = new TagBuilder("td");
                    td.InnerHtml.AppendHtml(prope.GetValue(item).ToString());
                    tr.InnerHtml.AppendHtml(td);
                }
                table.InnerHtml.AppendHtml(tr);
            }

            div.InnerHtml.AppendHtml(table.InnerHtml);

            TagBuilder pagination = new TagBuilder("nav");
            pagination.Attributes["aria-label"] = "Page navigation";
            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");
            TagBuilder li = new TagBuilder("li");
            TagBuilder a = new TagBuilder("a");
            a.Attributes["href"] = $"{ActionUrl}?pagesize={PageSize}&&pageNum={PageNumber+1}";
            a.InnerHtml.AppendHtml("&laquo;");
            a.Attributes["aria-label"] = "Previous";
            li.InnerHtml.AppendHtml(a);
            ul.InnerHtml.AppendHtml(li);

            for (int p = 1; p <= PageShowNums; p++) {
                li = new TagBuilder("li");

                a = new TagBuilder("a");
                if (p == PageNumber) {
                    a.AddCssClass("active");
                }
                a.Attributes["href"] = $"{ActionUrl}?pagesize={PageSize}&&pageNum={p}";

                a.InnerHtml.Append(p.ToString());
                li.InnerHtml.AppendHtml(a);
                ul.InnerHtml.AppendHtml(li);
            }
            li = new TagBuilder("li");
            a = new TagBuilder("a");
            a.Attributes["href"] = $"{ActionUrl}?pagesize={PageSize}&&pageNum={(PageNumber>1? PageNumber:PageNumber-1)}";
            a.InnerHtml.AppendHtml("&raquo;");
            a.Attributes["aria-label"] = "Next";
            li.InnerHtml.AppendHtml(a);
            ul.InnerHtml.AppendHtml(li);
            pagination.InnerHtml.AppendHtml(ul);
            
            div.InnerHtml.AppendHtml(pagination.InnerHtml);
            output.Content.AppendHtml(div.InnerHtml);
           
        }

    }
}
