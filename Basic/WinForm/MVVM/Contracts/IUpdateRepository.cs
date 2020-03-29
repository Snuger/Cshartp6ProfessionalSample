using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUpdateRepository<T,in Tkey> where T:class
    {
        Task<T> AddAsync(T item);

        Task<T> UpdateAsync(T item);

        Task<bool> DelteAsync(int id);
    }
}
