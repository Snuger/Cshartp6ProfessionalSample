using System;
using System.Collections.Generic;
using System.Text;

namespace Template.API.Services
{
    public interface IPageService
    {
        int GetPageSize();
        int GetPageIndex();
    }
}
