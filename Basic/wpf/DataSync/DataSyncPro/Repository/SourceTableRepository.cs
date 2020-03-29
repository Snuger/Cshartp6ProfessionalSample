using DataSyncPro.Contract.IRepository;
using DataSyncPro.Db;
using DataSyncPro.Db.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace DataSyncPro.Repository
{
    public class SourceTableRepository : ISourceTableRepository
    {
        public Task<bool> Add(SourceTable model, Action<SourceTable, Exception> callBack)
        {
            bool result = false;
            try
            {
                using (DataSyncContext context = new DataSyncContext())
                {
                    context.SourceTables.Add(model);
                    result=context.SaveChanges()>0?true:false;                    
                }
                callBack(model, null);
            }
            catch (Exception ex)
            {
                callBack(model, ex);               
            }
            return Task.FromResult<bool>(result);           
        }

        public Task<bool> Delete(int key, Action<int, Exception> callBack)
        {
            bool result = false;
            try
            {
                using (DataSyncContext context = new DataSyncContext())
                {
                    result= context.SourceTables.Where(c => c.ID == key).Delete()>0?true:false;
                    context.SaveChanges();
                }
                callBack(key, null);
            }
            catch (Exception ex)
            {
                callBack(key, ex);
            }
            return Task.FromResult<bool>(result);            
        }

        public SourceTable GetItemByID(int id)
        {
            using (DataSyncContext context = new DataSyncContext()) {
              return context.SourceTables.Where(c => c.ID == id).FirstOrDefault();
            }
        }

        public IEnumerable<SourceTable> GetItems()
        {
            using (DataSyncContext context = new DataSyncContext())
            {
                return context.SourceTables;
            }
        }

        public Task<IEnumerable<SourceTable>> GetItemsAsync()
        {
            using (DataSyncContext context = new DataSyncContext())
            {
                return Task.FromResult((IEnumerable<SourceTable>)context.SourceTables);
            }
        }

        public Task<SourceTable> GetItemsByIDAsync(int id)
        {
            using (DataSyncContext context = new DataSyncContext())
            {
                return Task.FromResult(context.SourceTables.Where(c => c.ID == id).FirstOrDefault());
            }
        }

        public Task<bool> Update(SourceTable model, Action<SourceTable, Exception> callBack)
        {
            bool result = false;
            try
            {
                using (DataSyncContext context =new DataSyncContext())
                {
                    context.Entry<SourceTable>(model).State = System.Data.Entity.EntityState.Modified;
                    result= context.SaveChanges()>0?true:false;
                }
                callBack(model, null);
            }
            catch (Exception ex)
            {
                callBack(model, ex);             
            }
            return Task.FromResult(result);
        }
    }
}
