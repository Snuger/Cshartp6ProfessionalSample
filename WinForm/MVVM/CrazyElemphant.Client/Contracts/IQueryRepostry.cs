using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyElemphant.Client.Contracts
{
    public interface IQueryRepostry<T,in Tkey> where T:class
    {
        Task<T> GetItemsByIDAsync(Tkey id);

        T GetItemByID(Tkey id);

        Task<T> GetItemsAsync();

        List<T> GetItems();
        
    }
}
