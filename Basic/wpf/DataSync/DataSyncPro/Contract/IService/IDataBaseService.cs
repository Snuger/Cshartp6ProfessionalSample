using DataSyncPro.Db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncPro.Contract.IService
{
    public interface IDataBaseService
    {
        Task<bool> Add(SynchronousDb synchronousDb);

        Task<bool> Update(SynchronousDb synchronousDb);

       IEnumerable<SynchronousDb>GetSynchronousDbs();

        Task<bool> Delete(int ID);
    }
}
