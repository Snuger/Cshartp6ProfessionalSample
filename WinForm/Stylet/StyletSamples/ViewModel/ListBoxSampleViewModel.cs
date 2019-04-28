using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Stylet;

namespace StyletSamples.ViewModel
{
    public class ListBoxSampleViewModel:Screen
    {

        private string currentSelect;

        public string CurrentSelect
        {
            get { return currentSelect; }
            set
            {
                SetAndNotify(ref currentSelect, value);
            }
        }


        private List<string> language;

        public ListBoxSampleViewModel()
        {
            Languages = new List<string>() {"C#","JAVA","JavaScript","TypeScript" };
        }

        public List<string> Languages
        {
            get { return language; }
            set
            {
                SetAndNotify(ref this.language, value);
            }
        }

        public void GetSelectItem(object sender)
        {
            ListBox listbox = sender as ListBox;
            CurrentSelect = listbox.SelectedItem.ToString();          
        }

    }
}
