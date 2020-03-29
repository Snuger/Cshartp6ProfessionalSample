using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncPro.Contract
{
    public interface IUpdateRepostry<T,in TKey> where T :class
    {
        Task<bool> Add(T model,Action<T, Exception> callBack);

        Task<bool> Update(T model,Action<T, Exception> callBack);

        Task<bool> Delete(TKey key,Action<int, Exception> callBack);

    }
}
