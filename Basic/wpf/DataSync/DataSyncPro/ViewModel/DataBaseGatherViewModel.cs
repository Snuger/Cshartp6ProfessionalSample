using DataSyncPro.UserControls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DataSyncPro.Contract.IService;
using DataSyncPro.Db;
using System.Collections.ObjectModel;
using DataSyncPro.ViewModel.DBViewModel;
using DataSyncPro.Model;
using DataSyncPro.Model.ViewModel;
using System.Threading;

namespace DataSyncPro.ViewModel
{
    public class DataBaseGatherViewModel:ViewModelBase
    {
        private IDataBaseService dataBaseService;

        public DataBaseGatherViewModel(IDataBaseService baseService)
        {
            this.dataBaseService = baseService;
            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<DataBaeConfigViewModel, SynchronousDb>()
                .ForMember(c=>c.Ip,opt=>opt.MapFrom(src=>src.Ip))
                .ForMember(c => c.Port, opt => opt.MapFrom(src => src.Port))
                .ForMember(c => c.DbType, opt => opt.MapFrom(src => src.DbType));
                cfg.CreateMap<SynchronousDbViewModel, SynchronousDb>()
                .ForMember(x => x.Port, opt => opt.MapFrom(src => src.DataBaseType.DbPort));             
                cfg.CreateMap<SynchronousDb, SynchronousDbViewModel>()               
                .ForMember(x => x.IsChecked, opt => opt.Ignore())
                .ForMember(x=>x.DataBaseType,opt=>opt.MapFrom(src=>SysConstant.dataBaseTypes.Where(c=>c.DatabseTypeId==src.DbType).FirstOrDefault()));
                cfg.CreateMap<SynchronousDbViewModel, DataBaeConfigViewModel>().ForMember(x=>x.SelectDataBaseType,opt=>opt.MapFrom(src=>src.DataBaseType));
            });
         
            //打开窗口
            OpenDbConfigDilogCommand = new RelayCommand<string>(parame=> this.OpenDbConfigDilog(parame));
            DataBaseGatherLoadedCommand = new RelayCommand(LoadData);
            //DeleteDataBaseConfigCommad = new RelayCommand(Delete);
        }

        public ICommand OpenDbConfigDilogCommand { get;  } 

        public ICommand SaveDbConfigDilogCommand { get; set; }

        public ICommand DataBaseGatherLoadedCommand { get; set; }


        public ICommand DeleteDataBaseConfigCommad
        {
            get
            {
                return new RelayCommand<SynchronousDbViewModel>((model)=> {
                    Task<bool> result =dataBaseService.Delete(model.ID);
                    if (result.Result)
                        LoadData();
                });
            }
        }


        private IEnumerable<SynchronousDbViewModel> database;

        public IEnumerable<SynchronousDbViewModel> Database
        {
            get { return database; }
            set { Set(ref database, value); }
        }


        //public ICommand ShowCommand
        //{
        //    get
        //    {
        //        return new RelayCommand<string>(
        //            (user) =>
        //            {

        //            }, (user) =>
        //            {
        //                return !string.IsNullOrEmpty(user);
        //            });

        //    }
        //}


        private SynchronousDbViewModel currentSynchronousDB;

        public SynchronousDbViewModel CurrentSynchronousDB
        {
            get { return currentSynchronousDB; }
            set { Set(ref currentSynchronousDB, value); }
        }


        private bool isModify;

        public bool IsModify
        {
            get { return isModify; }
            set { Set(ref isModify, value); }
        }

        private async void LoadData()
        {
            var view = new LoadingPannel();
            var result = await DialogHost.Show(view, LoadingDialogOpenedEventHandler);    
            
        }

        private void LoadingDialogOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            IEnumerable<SynchronousDb> SynchronousDbs = dataBaseService.GetSynchronousDbs();
            Database = AutoMapper.Mapper.Map<IEnumerable<SynchronousDbViewModel>>(SynchronousDbs);
            eventArgs.Session.Close(false);
        }

        //private void LoadingDialogClosedEventHandler(object sender, DialogClosingEventArgs eventArgs)
        //{
        //    Thread.Sleep(3000);
            
        //}


        private async void Delete() {
            if (CurrentSynchronousDB == null)
                return;
           bool rsult=await dataBaseService.Delete(CurrentSynchronousDB.ID);
            if (rsult)
            {               
               LoadData();
            }
        }


        public ICommand ChangeDbStateCommand
        {
            get
            {
                return new RelayCommand<SynchronousDbViewModel>((model)=> {
                    model.Enable = model.Enable==1 ? 0 : 1;
                    SynchronousDb db = AutoMapper.Mapper.Map<SynchronousDb>(model);
                    dataBaseService.Update(db);
                });
            }
        }

        //private async void ChangeDbState(SynchronousDbViewModel model)
        //{
        //    model.Enable = model.Enable == 0 ? 1 : 0;
        //    SynchronousDb db = AutoMapper.Mapper.Map<SynchronousDb>(model);
        //    await dataBaseService.Update(db);
        //}

        private async void OpenDbConfigDilog(string opearType)
        {
            var view = new DataBaeConfigPanel();
            switch (opearType.ToLower())
            {
                case "add":
                    IsModify = false;
                    break;
                case "modify":
                    IsModify = true;
                    DataBaeConfigViewModel model = AutoMapper.Mapper.Map<DataBaeConfigViewModel>(CurrentSynchronousDB);
                    view.DataContext = model;
                    break;
            }           
            var result = await DialogHost.Show(view, DbConfigDilogCloseEventHanle);
        }
  
        private void DbConfigDilogCloseEventHanle(object Sender,DialogClosingEventArgs args) {
            if ((bool)args.Parameter == false) return;
            DataBaeConfigViewModel model = ((System.Windows.FrameworkElement)args.Session.Content).DataContext as DataBaeConfigViewModel;
            SynchronousDb db = AutoMapper.Mapper.Map<SynchronousDb>(model);
            switch (IsModify)
            {
                case true:
                    dataBaseService.Update(db);
                    break;
                case false:
                    dataBaseService.Add(db);
                    break;
                default:
                    dataBaseService.Update(db);
                    break;
            }
           
            LoadData();//重新加载数据         
        }
    }
}
