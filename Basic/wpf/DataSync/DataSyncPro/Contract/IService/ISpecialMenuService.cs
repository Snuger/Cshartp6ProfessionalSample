using DataSyncPro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncPro.Contract.IService
{
    public interface ISpecialMenuService
    {
        void GetBasicMenu(Action<IEnumerable<BasicMenu>, Exception> callback);
    }
}
