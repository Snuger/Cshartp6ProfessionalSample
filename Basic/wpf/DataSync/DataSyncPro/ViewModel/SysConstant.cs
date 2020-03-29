using DataSyncPro.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncPro.ViewModel
{
    public class SysConstant
    {
        public static ObservableCollection<DataBaseType> dataBaseTypes=new ObservableCollection<DataBaseType>() {
             new DataBaseType(){ DatabseTypeId=1, DatabseTypeName="Oracle",DbPort=1521 },
                 new DataBaseType(){ DatabseTypeId=2, DatabseTypeName="MySql",DbPort=3306 },
                 new DataBaseType(){ DatabseTypeId=3, DatabseTypeName="PostgreSql",DbPort=5432 },
                 new DataBaseType(){ DatabseTypeId=4, DatabseTypeName="SqlServer",DbPort=1433 },
                 new DataBaseType(){ DatabseTypeId=5, DatabseTypeName="HiBase",DbPort=60000 },
                 new DataBaseType(){ DatabseTypeId=6, DatabseTypeName="MangoDB",DbPort=27107 }
        };
    }
}
