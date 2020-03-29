using DataSync.Api;
using DataSync.Model;
using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSync.ViewModel
{
    public class MainViewModel:Screen
    {

        object lockobject = new object();

        public int MaxThreadNum { get; set; } = 5;

        protected DateTime BeginDate { get; set; } = new DateTime(2018, 1, 1);

        public MainViewModel()
        {
            this.CurrentNum = 0;
            this.DataCells = new BindableCollection<DataCell>();

            TaskThradManagement = new ThreadManagement(MaxThreadNum);
            TaskThradManagement.UploadCompontentComplatedEvent += OnUploadCompontentComplatedEvent;
        }       

        private bool isAutoModel;

        public bool IsAutoModel
        {
            get { return isAutoModel; }
            set
            {
                SetAndNotify(ref this.isAutoModel, value);
            }
        }

        private BindableCollection<DataCell> dataCells;

        public BindableCollection<DataCell> DataCells
        {
            get { return dataCells; }
            set
            {
                SetAndNotify(ref this.dataCells, value);
            }
        }

        private int currentNum;       

        public int CurrentNum
        {
            get { return currentNum; }
            set
            {
                SetAndNotify(ref this.currentNum, value);
            }
        }

        private int activeThreadNum;

        /// <summary>
        /// 现激活线程数
        /// </summary>
        public int ActiveThreadNum
        {
            get { return activeThreadNum; }
            set
            {
                SetAndNotify(ref this.activeThreadNum, value);
            }
        }

        public ThreadManagement TaskThradManagement { get; private set; }

        public void AddNewTaskComman() {
            CurrentNum = CurrentNum + 1;            
            DataCell dataCell=   new DataCell() { Id = this.CurrentNum, Name = "用户基本资料", Total = new Random().Next(1000, 10000), UploadedTotal = 0, BusinessDate = BeginDate.ToString("yyyy-MM-dd"), Complated = false };
            BeginDate = BeginDate.AddDays(1);
            lock (lockobject)
            {
                this.DataCells.Add(dataCell);
            }           
        }

        public void AllTaskStartCommand()
        {
            lock (lockobject)
            {
                foreach (var item in this.DataCells.ToList().Where(c => c.Complated == false))
                {
                    if (ActiveThreadNum < MaxThreadNum)
                    {
                        var query = TaskThradManagement.Compontents.Where(c => c.CurrentDataCell.Id == item.Id);
                        if (!query.Any())
                        {
                            UploadCompontent compontent = new UploadCompontent(item);
                            compontent.CompontentComplatedEvent += TaskThradManagement.OnCompontentComplated;
                            TaskThradManagement.Compontents.Add(compontent);
                            compontent.CreateCompontentWorkThrad();
                            compontent.Start();
                            ActiveThreadNum = ActiveThreadNum + 1;
                        }

                    }
                    else
                    {
                        break;
                    }

                }
            }            
        }

        protected void OnUploadCompontentComplatedEvent(DataCell cell)
        {            
            this.DataCells.Where(c => c.Id == cell.Id).FirstOrDefault().UploadedTotal = cell.UploadedTotal;
            this.DataCells.Refresh();
            if (cell.UploadedTotal >= cell.Total)
            {
                this.DataCells.Where(c => c.Id == cell.Id).FirstOrDefault().Complated = true;
                ActiveThreadNum = ActiveThreadNum - 1;
                if (IsAutoModel)
                {
                    AddNewTaskComman();
                    this.DataCells.Refresh();
                }
                AllTaskStartCommand();
            }         
        }



        /// <summary>
        /// 自动化加载数据
        /// </summary>
        protected void BatchLoading() {
            if (IsAutoModel)
            {
                for(int t = 0; t < MaxThreadNum; t++)
                {
                    AddNewTaskComman();
                }
            }          
        }

        public void FullyAutomaticCommand() {
            IsAutoModel = true;
            BatchLoading();
            AllTaskStartCommand();
        }

    }
}
