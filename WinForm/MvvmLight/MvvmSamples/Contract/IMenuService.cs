using MvvmSamples.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmSamples.Contract
{
    public interface IMenuService
    {
        void GetMenu(Action<FunctionMenu, Exception> callback);
    }
}
