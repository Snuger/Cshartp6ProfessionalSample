using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IQueryRepository<T,in TKey> where T :class
    {
        Task<T> GetItemAsync(TKey id);

        Task<IEnumerable<T>> GetItemsAsync();

    }
}
