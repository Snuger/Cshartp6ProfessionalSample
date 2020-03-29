using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncPro.Contract.IService
{
     public interface IUpdateService<T,Tkey> where T: class
    {
        bool Add(T model);

        bool Update(T model, Tkey tkey);
        
        bool Delete(Tkey key);

        bool Delete(T model);

    }
}
