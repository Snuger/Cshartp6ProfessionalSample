using DataSyncPro.Compontent;
using DataSyncPro.IService;
using DataSyncPro.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataSyncPro.ViewModel
{
    public class AutomaticUploadViewModel : ViewModelBase
    {
        protected object LockObject = new object();


        private string _automaticButtonText;

        public string AutomaticButtonText
        {
            get { return _automaticButtonText; }
            set { Set(ref _automaticButtonText, value); }
        }

        private int id;

        public int Id
        {
            get { return id; }
            set {
                Set(ref id, value);
            }
            
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        protected DateTime BeginDate { get; set; } = new DateTime(2018, 1, 1);
        /// <summary>
        /// 最大的线程数
        /// </summary>
        public int MaxThreadNum { get; set; } =5;

        /// <summary>
        /// 
        /// </summary>
        public ThreadManagement ThreadManagement { get; private set; }


        private readonly IDataService _dataService;

        /// <summary>
        /// 自动上传
        /// </summary>
        public ICommand AutomaticUploadCommand { get; set; }

        /// <summary>
        /// 启动所有线程
        /// </summary>
        public ICommand StatrtAllUploadThreadCommand { get; set; }

        public AutomaticUploadViewModel(IDataService dataService)
        {
            DispatcherHelper.Initialize();
            AutomaticButtonText = "启动自动上传";
            AutoRuning = false;
            _dataService = dataService;
            AutomaticUploadCommand = new RelayCommand(AutomaticUpload);
            this.ThreadManagement = new ThreadManagement(MaxThreadNum);
            this.ThreadManagement.UploadCompontentComplatedEvent += UploadCompontentComplated;          
            this.UploadEnities = new ObservableCollection<UploadEntity>();

            _dataService.GetUploadDataOptions(5, (item, error) =>
            {
                if (error != null)
                    return;
                this.UploadDataOptions = item;
            });
        }


        private ObservableCollection<UploadEntity> uploadEnities;

        public ObservableCollection<UploadEntity> UploadEnities
        {
            get { return uploadEnities; }
            set { Set(ref uploadEnities, value); }
        }

        protected void AutomaticUpload()
        {
            IsAutomatic = true;
            AutoRuning =AutoRuning==true ? false : true;              
            AutomaticButtonText = (AutoRuning) ? "停止自动上传" : "启动自动上传";
            if (AutoRuning)
            {
                AutomaticLoadingData();
                StatrtAllUploadThread(-1);
            }
        }


        private int workingThreadNum;

        /// <summary>
        /// 工作中的线程数
        /// </summary>
        public int WorkingThreadNUm
        {
            get { return workingThreadNum; }
            set { Set(ref workingThreadNum, value); }
        }


        private bool isAutomatic;

        /// <summary>
        /// 是否自动模式
        /// </summary>
        public bool IsAutomatic
        {
            get { return isAutomatic; }
            set { Set(ref isAutomatic, value); }
        }


        private bool autoRuning;

        /// <summary>
        /// 自动上传的运行状态
        /// </summary>
        public bool AutoRuning
        {
            get { return autoRuning; }
            set { Set(ref autoRuning, value); }
        }

        private int _completedNum;

        public int CompletedNum
        {
            get { return _completedNum; }
            set { Set(ref _completedNum, value); }
        }

        private long _uploadedTotal;

        public long UploadedTotal
        {
            get { return _uploadedTotal; }
            set { Set(ref _uploadedTotal, value); }
        }

        private IEnumerable<UploadDataOption> uploadDataOptions;

        /// <summary>
        ///待上传数据
        /// </summary>
        public IEnumerable<UploadDataOption> UploadDataOptions
        {
            get { return uploadDataOptions; }
            set { Set(ref uploadDataOptions, value); }
        }


        protected void AutomaticLoadingData()
        {
            if (IsAutomatic)
            {
                foreach (UploadDataOption option in UploadDataOptions)
                {
                    lock (LockObject)
                    {
                        option.Id = Id;
                        option.OperatingRange = BeginDate.ToString("yyyy-MM-dd");
                        _dataService.GetData(option, (itemes, error) =>
                        {
                            if (error != null)
                                return;
                            foreach (var item in itemes)
                            {
                                UploadEnities.Add(item);
                            }                           
                        });
                        Id = Id + 1;
                    }
                }

            }
        }

        protected void StatrtAllUploadThread(int id=-1)
        {
            var query = this.UploadEnities.Where(c =>c.IsComplated == false);
            if (id != -1)
                query = query.Where(c=>c.Id==id);
            if (query.Any())
            {
                foreach (var entity in query)
                {
                    if (workingThreadNum < MaxThreadNum)
                    {
                        UploadCompontent compontent = new UploadCompontent(entity);
                        compontent.UploadCompontentComplatedEvent += this.ThreadManagement.OnCompontentComplated;
                        compontent.CreateCompontentWorkThrad();
                        this.ThreadManagement.UploadCompontents.Add(compontent);
                        compontent.Start();
                        WorkingThreadNUm = WorkingThreadNUm + 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }

        }

        public void UploadCompontentComplated(UploadEntity entity)
        {
            int newThreadId = 0;
            lock (LockObject)
            {
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    this.UploadEnities.Where(xc => xc.Id == entity.Id).FirstOrDefault().Uploaded = entity.Uploaded;
                    if (entity.Uploaded >= entity.Total)
                    {
                        this.UploadEnities.Where(xc => xc.Id == entity.Id).FirstOrDefault().IsComplated = true;
                        WorkingThreadNUm = WorkingThreadNUm - 1;
                        CompletedNum = CompletedNum + 1;
                        if (IsAutomatic)
                        {                            
                            if (AutoRuning)
                            {
                                Id = Id + 1;
                                newThreadId = Id;
                                UploadDataOption option = new UploadDataOption()
                                {
                                    Id = newThreadId,
                                    IsComplated = false,
                                    TableName = entity.TableName,
                                    TableDiscription = entity.TableDiscription,
                                    OperatingRange = DateTime.ParseExact(entity.OperatingRange.ToString(), "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture).AddDays(1).ToString("yyyy-MM-dd")
                                };

                                _dataService.GetData(option, (items, error) =>
                                {

                                    if (error != null)
                                        return;
                                    foreach (var item in items)
                                    {
                                        this.UploadEnities.Add(item);
                                    }

                                });
                            }
                            
                        }

                       StatrtAllUploadThread(newThreadId);
                    }
                    //合计总数量
                    UploadedTotal = this.UploadEnities.Sum(c => c.Uploaded);
                });

            }
        }
    }
}
