using GalaSoft.MvvmLight;
using MvvmSamples.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmSamples.ViewModel
{
    public class CustomMenuViewModel: ViewModelBase
    {
        private FunctionMenu _menu;

        public FunctionMenu Menu
        {
            get { return _menu; }
            set
            {
                Set(ref _menu, value);
            }
        }

    }
}
