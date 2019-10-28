using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EvironmentSample30.GitHub
{
    public class GithubService
    {
        public GithubService(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://api.github.com/");
            httpClient.DefaultRequestHeaders.Add("Accept","application/vnd.github.v3+json");
            httpClient.DefaultRequestHeaders.Add("User-Agent","HttpClientFactory-Sample");
            HttpClient = httpClient;
        }

        public HttpClient HttpClient { get;}

        public async Task<IEnumerable<GitHubIssue>> GetAspNetDocsIssues() { 
            var response=await HttpClient.GetAsync("/repos/aspnet/AspNetCore.Docs/issues?state=open&sort=created&direction=desc");
            if (response.IsSuccessStatusCode)
            {
              return   await response.Content.ReadAsAsync<IEnumerable<GitHubIssue>>();
            }
            else {
                return null;
            }
        }

    }
}
