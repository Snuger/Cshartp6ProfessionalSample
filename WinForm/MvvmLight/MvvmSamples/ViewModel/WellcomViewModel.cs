using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmSamples.ViewModel
{
    public class WellcomViewModel : ViewModelBase
    {
        private string _wellcomeTitel;

        public WellcomViewModel()
        {
            WellcomTitle = "这是一个简单的欢迎页面";
            Author = "Snuger";
            TestCommond = new RelayCommand(TestCommondMethod);
        }

        public string WellcomTitle
        {
            get { return _wellcomeTitel; }
            set
            {
                Set(ref _wellcomeTitel, value);
            }
        }


        private string author;

        public string Author
        {
            get { return author; }
            set
            {
                Set(ref author, value);
            }
        }

        public ICommand TestCommond { get; private set; }

        public void TestCommondMethod()=> Author = $"{Author},{System.DateTime.Now.ToString("yyyy-MM-dd")}";
        
    }
}
