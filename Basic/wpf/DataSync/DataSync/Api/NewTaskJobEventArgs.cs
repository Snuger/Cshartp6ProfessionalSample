using DataSync.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSync.Api
{
    public class NewTaskJobEventArgs:EventArgs
    {
        public NewTaskJobEventArgs(List<DataCell> dataCells)
        {
            DataCells = dataCells;
        }

        public List<DataCell> DataCells { get; set; }
    }
}
