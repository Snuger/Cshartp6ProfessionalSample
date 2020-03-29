using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncPro.Contract
{
    public interface IQueryRepostry<T,in TKey> where T :class
    {
        Task<T> GetItemsByIDAsync(TKey id);

        T GetItemByID(TKey id);

        Task<IEnumerable<T>> GetItemsAsync();

        IEnumerable<T> GetItems();
    }
}
