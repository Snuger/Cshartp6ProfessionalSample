using DataSyncPro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataSyncPro.Compontent
{
    public class UploadCompontent
    {
        public delegate void UploadCompontentComplatedHandle(UploadEntity entity);
        public event UploadCompontentComplatedHandle UploadCompontentComplatedEvent;

        public UploadCompontent(UploadEntity uploadEntity)
        {
            CurrentUploadEntity = uploadEntity;
            this.UploadThreadNum = uploadEntity.Id;
            this.IsComplated = false;
        }

        /// <summary>
        /// 线程数
        /// </summary>
        protected int UploadThreadNum { get; set; }

        /// <summary>
        /// 当前线程工作的任务是否完成
        /// </summary>
        protected bool IsComplated { get; set; }

        /// <summary>
        /// 工作线程
        /// </summary>
        protected Thread WorkingThread { get; set; }


        public UploadEntity CurrentUploadEntity { get; set; }


        public void CreateCompontentWorkThrad()
        {
            WorkingThread = new Thread(new ThreadStart(CompontentDoWork));
            WorkingThread.IsBackground = true;
        }

        protected void CompontentDoWork()
        {
            while (true)
            {
                Random random = new Random(10);
                long tick = DateTime.Now.Ticks;
                int uploadTotal = CurrentUploadEntity.Uploaded + new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32)).Next(1000, 5500);
                CurrentUploadEntity.Uploaded = uploadTotal >= CurrentUploadEntity.Total ? CurrentUploadEntity.Total : uploadTotal;
                UploadCompontentComplatedEvent?.Invoke(CurrentUploadEntity);
                Thread.Sleep(new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32)).Next(50, 2000));
            }
        }


        /// <summary>
        /// 启动
        /// </summary>
        public void Start() =>WorkingThread?.Start();

        /// <summary>
        /// 
        /// </summary>
        public void Join() => WorkingThread?.Join();

        /// <summary>
        /// 挂起线程
        /// </summary>
        public void Suspend() => WorkingThread?.Suspend();


        /// <summary>
        /// 恢复
        /// </summary>
        public void Resume() => WorkingThread?.Resume();

        /// <summary>
        /// 终止线程
        /// </summary>
        public void Abort() => WorkingThread?.Abort();

    }
}
