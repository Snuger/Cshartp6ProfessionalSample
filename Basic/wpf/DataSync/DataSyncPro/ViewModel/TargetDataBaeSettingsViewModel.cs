using DataSyncPro.Model;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSyncPro.UserControls;

namespace DataSyncPro.ViewModel
{
    public class TargetDataBaeSettingsViewModel
    {
        private List<BasicMenu> _menus;

        public List<BasicMenu> Menus
        {
            get { return _menus; }
            set { _menus = value; }
        }

        public TargetDataBaeSettingsViewModel()
        {
            Menus = new List<BasicMenu>() {
                 new BasicMenu(){Id="1", Name="数据库设置",Discription="设置现有的可用数据库", Kiind=PackIconKind.Database, ParentId="0", Content=new TartgetDatabaseGatherPanel() },
                 new BasicMenu(){Id="1", Name="数据表设置",Discription="设置现有的可用数据表", Kiind=PackIconKind.Database, ParentId="0", Content=new TartgetTableGatherPanel() }
            };
        }
    }
}
