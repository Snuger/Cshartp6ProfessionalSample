using DataSyncPro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncPro.Compontent
{
    public class ThreadManagement
    {
        /// <summary>
        /// 当任务完成时
        /// </summary>
        /// <param name="entity"></param>
        public delegate void UploadCompontentComplatedHandle(UploadEntity entity);
        public event UploadCompontentComplatedHandle UploadCompontentComplatedEvent;


        public ThreadManagement(int maxThreadNum)
        {
            MaxThreadNum = maxThreadNum;
            this.UploadCompontents = new List<UploadCompontent>();
         
        }

        /// <summary>
        /// 最大线线程数
        /// </summary>
        public int MaxThreadNum { get; set; }

        public List<UploadCompontent> UploadCompontents { get; set; }



        public void StartAllThread() {
            UploadCompontents?.ForEach((c)=>{
                if (c.CurrentUploadEntity.IsComplated == false)
                    c.Start();
            });
        }

        public void OnCompontentComplated(UploadEntity entity)
        {
            UploadCompontent _compontent = this.UploadCompontents.Where(c => c.CurrentUploadEntity == entity).FirstOrDefault();
            UploadCompontentComplatedEvent.Invoke(entity);
            if (entity.Total <= entity.Uploaded)
            {
                _compontent.Abort();
               // compontent.Join();
            }
        }


    }
}
