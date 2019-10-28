using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvironmentSample30.GitHub
{
    public class GitHubIssue
    {
        [JsonProperty(PropertyName = "html_url")]
        public string Url { get; set; }

        public string Title { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime Created { get; set; }
    }
}