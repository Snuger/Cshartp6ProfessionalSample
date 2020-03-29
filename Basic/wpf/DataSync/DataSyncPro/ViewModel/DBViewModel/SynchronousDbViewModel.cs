using DataSyncPro.Model.ViewModel;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncPro.ViewModel.DBViewModel
{
    public class SynchronousDbViewModel:ViewModelBase
    {
        public int ID { get; set; }


        public string Ip { get; set; }


        private DataBaseType _dataBaseType;

        public DataBaseType DataBaseType
        {
            get { return _dataBaseType; }
            set { Set(ref _dataBaseType, value); }
        }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public string InstanceName { get; set; }

        public int Enable { get; set; }

        public bool IsChecked { get; set; }
    }
}
