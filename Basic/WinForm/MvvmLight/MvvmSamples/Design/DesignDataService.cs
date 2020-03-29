using System;
using MvvmSamples.Model;

namespace MvvmSamples.Design
{
    public class DesignDataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {     
            var item = new DataItem("Welcome to MVVM Light [design]");
            callback(item, null);
        }
    }
}