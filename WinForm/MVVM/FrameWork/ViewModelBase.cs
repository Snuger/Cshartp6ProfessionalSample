using System;
using System.Collections.Generic;
using System.Text;

namespace FrameWork
{
    public class ViewModelBase:BindableBase
    {
        private bool isLoad;
        public bool IsLoad {
            get { return IsLoad; }
            set
            {
                isLoad = value;
               this.RaisePropertyChanged("IsLoad");
            }
        }

        private bool update;



        public bool Update {
            get {
                return update;
            }
            set {
                update = value;
                RaisePropertyChanged("Update");
            }
        }



    }
}
