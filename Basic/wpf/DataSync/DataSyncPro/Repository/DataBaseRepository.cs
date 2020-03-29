using DataSyncPro.Db;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSyncPro.Contract.IRepository;
using DataSyncPro.Contract;
using Z.EntityFramework.Plus;

namespace DataSyncPro.Repository
{
    public class DataBaseRepository : IDataBaseRepository
    {
        public Task<bool> Delete(int key, Action<int, Exception> callBack)
        {
            bool result = false;
            try
            {
                using (DataSyncContext context = new DataSyncContext())
                {
                    context.SynchronousDb.Where(c => c.ID == key).Delete();
                    result =  context.SaveChanges() > 0 ? true : false;
                }
                callBack(key, null);
            }
            catch (Exception ex)
            {
                callBack(key, ex);
            }           
            return Task.FromResult(result);
        }

        public SynchronousDb GetItemByID(int id)
        {
            using (DataSyncContext context = new DataSyncContext())
            {
                return context.SynchronousDb.Where(c => c.ID == id).FirstOrDefault();
            }
        }

        public IEnumerable<SynchronousDb> GetItems()
        {           
            using (DataSyncContext context = new DataSyncContext()) {
                return context.SynchronousDb.ToList();
            }                      
        }

        public Task<SynchronousDb> GetItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SynchronousDb> GetItemsByIDAsync(int id)
        {
            throw new NotImplementedException();
        }


        Task<bool> IUpdateRepostry<SynchronousDb, int>.Add(SynchronousDb model, Action<SynchronousDb, Exception> callBack)
        {
            bool result = false;
            try
            {
                using (DataSyncContext context = new DataSyncContext())
                {
                    context.SynchronousDb.Add(model);
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

        Task<IEnumerable<SynchronousDb>> IQueryRepostry<SynchronousDb, int>.GetItemsAsync()
        {
            using (DataSyncContext context = new DataSyncContext())
            {
                return Task.FromResult((IEnumerable<SynchronousDb>)context.SynchronousDb.ToList());
            }
        }

        Task<bool> IUpdateRepostry<SynchronousDb, int>.Update(SynchronousDb model, Action<SynchronousDb, Exception> callBack)
        {
            bool result = false;
            try
            {
                using (DataSyncContext context = new DataSyncContext())
                {
                    //var obj=context.SynchronousDb.Where(c => c.ID == model.ID).FirstOrDefault();
                    //  obj = AutoMapper.Mapper.Map<SynchronousDb>(model);                    
                    context.Entry<SynchronousDb>(model).State = System.Data.Entity.EntityState.Modified;
                    result= context.SaveChanges()>0?true:false;
                    callBack(model, null);
                }
            }
            catch (Exception ex)
            {
                callBack(model, ex);
            }
            return Task.FromResult(result);
        }
    }
}
