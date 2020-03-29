using DataSync.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSync.Api
{
    public class ThreadManagement
    {

        public delegate void UploadCompontentComplatedHandle(DataCell cell);
        public event UploadCompontentComplatedHandle UploadCompontentComplatedEvent;

        /// <summary>
        /// 最大线程数
        /// </summary>
        public int MaxThradNum { get; set; }

        /// <summary>
        /// 待进行的任务
        /// </summary>
        public List<UploadCompontent> Compontents = new List<UploadCompontent>();

        public ThreadManagement(int maxThradNum)
        {
            MaxThradNum = maxThradNum;
            Compontents = new List<UploadCompontent>();             
        }

        public void OnCompontentComplated(DataCell dataCell)
        {
            UploadCompontent compontent = this.Compontents.Where(c => c.CurrentDataCell == dataCell).FirstOrDefault();
            UploadCompontentComplatedEvent.Invoke(dataCell);
            if (dataCell.Total <= dataCell.UploadedTotal)
            {
               //compontent.Abort();
                compontent.Join();
            }
        }

        /// <summary>
        /// 启动所有线程
        /// </summary>
        public void StartAll() => Compontents.ForEach(c => c.Start());
    }
}
