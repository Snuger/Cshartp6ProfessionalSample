using Template.API.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.API.Services
{
    public class PageService : IPageService
    {
        private readonly HttpContext _httpContext;
        public PageService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }
        public int GetPageIndex()
        {
            int result = 1;
            string page = _httpContext.Request.Query[PaginationConstant.PageIndex];
            if (!string.IsNullOrWhiteSpace(page))
            {
                if (!int.TryParse(page, out result))
                {
                    throw new FormatException("页码只能是数字");
                }
            }
            string head_page = _httpContext.Request.Headers[PaginationConstant.PageIndex];
            if (!string.IsNullOrWhiteSpace(head_page))
            {
                if (!int.TryParse(head_page, out result))
                {
                    throw new FormatException("页码只能是数字");
                }
            }
            return result;
        }

        public int GetPageSize()
        {
            int result = 1;
            string pageSize = _httpContext.Request.Query[PaginationConstant.PageSize];
            if (!string.IsNullOrWhiteSpace(pageSize))
            {
                if (!int.TryParse(pageSize, out result))
                {
                    throw new FormatException("页数只能是数字");
                }
            }
            string head_pagesize = _httpContext.Request.Headers[PaginationConstant.PageSize];
            if (!string.IsNullOrWhiteSpace(head_pagesize))
            {
                if (!int.TryParse(head_pagesize, out result))
                {
                    throw new FormatException("页数只能是数字");
                }
            }
            return result;
        }
    }
}
