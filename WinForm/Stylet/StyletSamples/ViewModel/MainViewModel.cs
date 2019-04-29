﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Stylet;
using StyletSamples.ViewModel;
using StyletSamples.Model;


namespace StyletSamples.ViewModel
{
    public class MainViewModel : Screen
    {
        
        public ListBoxSampleViewModel ListBoxInnerSampleViewModel { get; private set; }
       
        private Page currentPage = PageManager.PageWellcome;

        public MainViewModel()
        {
            this.ContentControlCartes = new List<Carte>() {
               new Carte(){ Name="Button Sample",Page=PageManager.ButtonSample},
               new Carte(){ Name="边框", Page=PageManager.BorderSamples },
               new Carte(){ Name="文本框",Page=PageManager.TextBoxSamples},
               new Carte(){ Name="ListBox Sample",Page=PageManager.ListBoxSample  
               }
            };

            this.HeaderContentControlCartes = new List<Carte>() {
               new Carte(){ Name="默认",Page=PageManager.PageWellcome},
               new Carte(){ Name="边框", Page=PageManager.BorderSamples },
               new Carte(){ Name="文本框",Page=PageManager.TextBoxSamples},
               new Carte(){ Name="ListBox Sample",Page=PageManager.ListBoxSample
               }
            };

            this.ListBoxInnerSampleViewModel = new ListBoxSampleViewModel();
        }

        public Page CurrentPage
        {
            get { return currentPage; }
            set { SetAndNotify(ref currentPage, value); }
        }

        public List<Carte> ContentControlCartes { get; set; }

        public List<Carte> HeaderContentControlCartes { get; set; }


        public void MenuSelected(object obj)
        {
            if (obj != null)
            {
                ListBoxItem item = obj as ListBoxItem;
               Carte carte= item.Content as Carte;
               CurrentPage = carte.Page;
            }        
        }

    }
}
