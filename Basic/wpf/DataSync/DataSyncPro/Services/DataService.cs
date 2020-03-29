using DataSyncPro.IService;
using System;
using System.Collections.Generic;

namespace DataSyncPro.Model
{
    public class DataService : IDataService
    {
        public void GetData(Action<IEnumerable<UploadEntity>, Exception> callback)
        {
            List<UploadEntity> uploadEntities = new List<UploadEntity>() {
               new UploadEntity(){Id=1, TableName="User", TableDiscription="用户基本资料", OperatingRange="2019-01-01", Total=new Random().Next(1000,900000), Uploaded=0},
               new UploadEntity(){Id=2, TableName="User", TableDiscription="用户基本资料", OperatingRange="2019-01-02", Total=new Random().Next(1000,900000), Uploaded=0},
               new UploadEntity(){Id=3, TableName="User", TableDiscription="用户基本资料", OperatingRange="2019-01-03", Total=new Random().Next(1000,900000), Uploaded=0},
               new UploadEntity(){Id=4, TableName="User", TableDiscription="用户基本资料", OperatingRange="2019-01-04", Total=new Random().Next(1000,900000), Uploaded=0},
               new UploadEntity(){Id=5, TableName="User", TableDiscription="用户基本资料", OperatingRange="2019-01-05", Total=new Random().Next(1000,900000), Uploaded=0},
               new UploadEntity(){Id=6, TableName="User", TableDiscription="用户基本资料", OperatingRange="2019-01-06", Total=new Random().Next(1000,900000), Uploaded=0},
               new UploadEntity(){Id=7, TableName="User", TableDiscription="用户基本资料", OperatingRange="2019-01-07", Total=new Random().Next(1000,900000), Uploaded=0},
           };
            callback(uploadEntities, null);
        }

        public void GetData(UploadDataOption option, Action<IEnumerable<UploadEntity>, Exception> callback)
        {
            Random random = new Random(10);
            long tick = DateTime.Now.Ticks;
            List<UploadEntity> uploadEntities = new List<UploadEntity>() {
               new UploadEntity(){Id=option.Id, TableName=option.TableName, TableDiscription=option.TableDiscription, OperatingRange=option.OperatingRange, Total=new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32)).Next(1000,90000), Uploaded=0},
           };
           callback(uploadEntities, null);
        }

        public void GetUploadDataOptions(int MaxThreadNum, Action<IEnumerable<UploadDataOption>, Exception> callback)
        {
            List<UploadDataOption> options = new List<UploadDataOption>() {
                new UploadDataOption(){ Id=1, TableName="User",TableDiscription="用户基本资料"},
                new UploadDataOption(){Id=2,TableName="Interpersonal",TableDiscription="人际关系"},
                new UploadDataOption(){Id=3,TableName="Admission",TableDiscription="入学经历"},
                new UploadDataOption(){Id=4,TableName="PurchaseRecord",TableDiscription="个人购买记录"},
                new UploadDataOption(){Id=5,TableName="PersonalIncident",TableDiscription="个人重要事件"}
            };
            callback(options, null);            
        }
    }
}