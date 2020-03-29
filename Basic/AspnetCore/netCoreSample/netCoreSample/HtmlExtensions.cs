using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netCoreSample
{
    public static class HtmlExtensions
    {
        public static string Div(this string value) =>$"<div>{value}</div>";
        public static string Span(this string value) =>$"<span>{value}</span>";
    }
}
