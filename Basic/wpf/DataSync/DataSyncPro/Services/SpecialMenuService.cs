using DataSyncPro.Contract;
using DataSyncPro.Contract.IService;
using DataSyncPro.Model;
using DataSyncPro.UserControls;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncPro.Model
{
    public class SpecialMenuService : ISpecialMenuService
    {
        public void GetBasicMenu(Action<IEnumerable<BasicMenu>, Exception> callback)
        {
            List<BasicMenu> basicMenus = new List<BasicMenu>() {
                 new BasicMenu(){Id="1", Name="系统设置", ParentId="0", Kiind=PackIconKind.Settings,Content=new WellcomPanel()},
                  new BasicMenu(){Id="2", Name="数据源设置", ParentId="0", Kiind=PackIconKind.DatabaseExport,Content=new DataSourceSettinsPanel()},
                  new BasicMenu(){Id="4", Name="目标数据源", ParentId="0", Kiind=PackIconKind.DatabaseImport,Content=new TargetDataBaeSettingsPanel()},
                  new BasicMenu(){Id="3", Name="数据同步", ParentId="0", Kiind=PackIconKind.UploadNetwork, Content=new AutomaticUploadPnel()},
                  new BasicMenu(){Id="5", Name="用户设置", ParentId="0", Kiind=PackIconKind.SettingsBox},
                  new BasicMenu(){Id="6", Name="用户设置", ParentId="0", Kiind=PackIconKind.RouterWirelessSettings},
                  new BasicMenu(){Id="7", Name="用户设置", ParentId="0", Kiind=PackIconKind.SettingsHelper}
            };
            callback(basicMenus, null);
        }
    }
}
