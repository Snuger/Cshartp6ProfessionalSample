using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace AspnetCoreExample.Services
{
    public interface IValuesService
    {
        Task<IEnumerable<string>> GetAll();

        Task<string> GetValueById(int id);
    }
}