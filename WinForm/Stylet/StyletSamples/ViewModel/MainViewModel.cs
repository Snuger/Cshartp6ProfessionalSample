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
        

        private Page currentPage = PageManager.PageWellcome;

        public MainViewModel()
        {
            this.Cartes = new List<Carte>() {
                new Carte(){ Name="默认",Page=PageManager.PageWellcome},
               new Carte(){ Name="边框", Page=PageManager.BorderSamples },
               new Carte(){ Name="文本框",Page=PageManager.TextBoxSamples}
               
            };
        }

        public Page CurrentPage
        {
            get { return currentPage; }
            set { SetAndNotify(ref currentPage, value); }
        }

        public List<Carte> Cartes { get; set; }


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
