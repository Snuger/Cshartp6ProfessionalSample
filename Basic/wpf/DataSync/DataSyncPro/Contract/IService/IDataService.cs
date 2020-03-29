using DataSyncPro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSyncPro.Db;

namespace DataSyncPro.IService
{
    public interface IDataService
    {
       
        void GetData(Action<IEnumerable<UploadEntity>, Exception> callback);


        void GetData(UploadDataOption option, Action<IEnumerable<UploadEntity>, Exception> callback);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        void GetUploadDataOptions(int MaxThreadNum,Action<IEnumerable<UploadDataOption>, Exception> callback);
    }
}
