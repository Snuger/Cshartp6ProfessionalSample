using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EvironmentSample30.GitHub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Net.Http.Formatting;

namespace EvironmentSample30.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public IEnumerable<GitHubBranch> Branches { get; private set; }

        public bool GetBranchesError { get; private set; }

        public PrivacyModel(ILogger<PrivacyModel> logger,IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGet()
        {
            var request=new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/repos/aspnet/AspNetCore.Docs/branches");
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Branches = await response.Content.ReadAsAsync<IEnumerable<GitHubBranch>>();
            }
            else {
               GetBranchesError = true;
               Branches = Array.Empty<GitHubBranch>();
            }

        }
    }
}
