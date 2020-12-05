using Refit;
using Starter.Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Template.API.RemoteServices.FunctionsSequence
{
    [BaseAddress("mcrp-functions-sequence")]
    public interface ISequenceService : IRemoteService
    {
        [Get("/mcrp-functions-sequence/api/ids/next")]
        Task<int> GetNewID(string catalog);
    }
}
