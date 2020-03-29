using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyElemphant.Client.Contracts
{
    public interface IUpdateRepostry<T, in TKey> where T : class
    {
        Task<T> Add(T item);

        Task<T> Delete(TKey id);

        Task<T> Update(T item);
    }
}
