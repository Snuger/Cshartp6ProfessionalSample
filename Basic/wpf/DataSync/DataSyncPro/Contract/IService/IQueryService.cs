using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncPro.Contract.IService
{
    public interface IQueryService<T,Tkey> where T:class
    {
        List<T> GetItems();

        T GetItem(Tkey tkey);
    }
}
