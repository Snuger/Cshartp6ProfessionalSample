using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MCRP.External.Jmeter.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public interface IJmeterPlanActuator
    {
        Task Execute(CommandOption option);

        Task<List<string[]>> ReadOrdinaryResult(CommandOption option);

    }
}
