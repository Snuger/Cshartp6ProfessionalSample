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
                OnPropertyChanged(nameof(IsLoad));
            }
        }

        private bool update;



        public bool Update {
            get {
                return update;
            }
            set {
                update = value;
                OnPropertyChanged(nameof(Update));
            }
        }



    }
}
