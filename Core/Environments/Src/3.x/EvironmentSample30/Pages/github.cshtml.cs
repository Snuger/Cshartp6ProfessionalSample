using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EvironmentSample30.GitHub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EvironmentSample30.Pages
{
    public class githubModel : PageModel
    {
        private IHttpClientFactory _httpClientFactory;

        public IEnumerable<GitHubPullRequest> PullRequests { get; private set; }

        public bool GetPullRequestsError { get; private set; }

        public bool HasPullRequests => PullRequests.Any();

        public githubModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task OnGet()
        {
          HttpClient client= _httpClientFactory.CreateClient("github");
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "repos/aspnet/AspNetCore.Docs/pulls");
            var response= await client.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                PullRequests = await response.Content.ReadAsAsync<IEnumerable<GitHubPullRequest>>();

            }
            else {
                GetPullRequestsError = false;
                PullRequests = Array.Empty<GitHubPullRequest>();
            }
        }
    }
}