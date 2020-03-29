using DataSync.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataSync.Api
{
    public class UploadCompontent
    {
        /// <summary>
        /// 任务执行完毕时
        /// </summary>
        /// <param name="dataCell"></param>
        public delegate void CompontentComplatedHadel(DataCell dataCell);
        public event CompontentComplatedHadel CompontentComplatedEvent;


        /// <summary>
        /// 线程数
        /// </summary>
        protected int UploadThreadNum { get; set; }

        /// <summary>
        /// 当前线程工作的任务是否完成
        /// </summary>
        protected bool IsComplated { get; set; }

        /// <summary>
        /// 当前线程工作的数据单元
        /// </summary>
        public DataCell CurrentDataCell { get; set; }

        /// <summary>
        /// 工作线程
        /// </summary>
        protected Thread WorkingTread { get; set; }


        public UploadCompontent(DataCell currentDataCell)
        {
            CurrentDataCell = currentDataCell;
            this.UploadThreadNum = CurrentDataCell.Id;
            this.IsComplated = false;
        }


        public void CreateCompontentWorkThrad()
        {
            WorkingTread = new Thread(new ThreadStart(CompontentDoWork));
            WorkingTread.IsBackground = true;          
        }

        protected void CompontentDoWork()
        {            
            while (true)
            {
                Random random = new Random(10);
                long tick = DateTime.Now.Ticks;
                int uploadTotal = CurrentDataCell.UploadedTotal + new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32)).Next(50, 700);
                this.CurrentDataCell.UploadedTotal = uploadTotal >= CurrentDataCell.Total?CurrentDataCell.Total: uploadTotal;
                CompontentComplatedEvent?.Invoke(this.CurrentDataCell);
                Thread.Sleep(new Random().Next(100, 1500));
            }
        }

        public void Start() => WorkingTread?.Start();

        public void Join() => WorkingTread?.Join();

        public void Suspend() => WorkingTread?.Suspend();


        /// <summary>
        /// 
        /// </summary>
        public void Resume() => WorkingTread?.Resume();

        /// <summary>
        /// 注销线程
        /// </summary>
        public void Abort() => WorkingTread?.Abort();

    }
}
