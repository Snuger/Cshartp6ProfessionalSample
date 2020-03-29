using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PureStyleSample.Pages
{
    public class Page1ViewModel:Screen
    {
        private string _testName;

        public Page1ViewModel()
        {
            TestName = "来来来，试试看";
        }

        public string TestName
        {
            get { return _testName; }
            set
            {
                SetAndNotify(ref this._testName, value);
            }
        }

    }
}
