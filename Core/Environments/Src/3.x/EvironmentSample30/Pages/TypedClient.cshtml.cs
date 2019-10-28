using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvironmentSample30.GitHub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EvironmentSample30.Pages
{
    public class TypedClientModel : PageModel
    {
        private readonly GithubService _service;
        public TypedClientModel(GithubService service)
        {
            _service = service;
        }

        public IEnumerable<GitHubIssue> Issues { get; set; }

        public async Task OnGet()
        {
            try
            {
                Issues = await _service.GetAspNetDocsIssues();
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}