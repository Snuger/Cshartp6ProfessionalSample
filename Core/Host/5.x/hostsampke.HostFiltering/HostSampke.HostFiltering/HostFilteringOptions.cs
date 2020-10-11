using System.Collections.Generic;

namespace HostSampke.HostFiltering
{
    public class HostFilteringOptions
    {
        public IList<string> AllowedHosts { get; set; } = new List<string>();

        public bool AllowEmptyHost { get; set; } = true;

        public bool IncludeFailureMessage { get; set; } = true;
    }
}