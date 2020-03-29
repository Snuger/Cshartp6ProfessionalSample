using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSync.Model;

namespace DataSync.Api
{
    public class DataCellQueue
    {
        protected event EventHandler<NewTaskJobEventArgs>NewTaskJobAddHadle;

        protected List<DataCell> DataCells { get; set; }

        public DataCellQueue()
        {
            DataCells = new List<DataCell>();
            NewTaskJobAddHadle += NewTaskJobsAdd;
        }

        /// <summary>
        /// 获取所有的
        /// </summary>
        /// <returns></returns>
        public List<DataCell> GetIncomplate() => DataCells.ToList();

        public void AddJobs(List<DataCell> dataCells)
        {
            NewTaskJobAddHadle?.Invoke(this, new NewTaskJobEventArgs(dataCells));
        }

        protected void NewTaskJobsAdd(object sender,NewTaskJobEventArgs e)
        {
            List<DataCell> dataCells =e.DataCells;
            DataCells.AddRange(dataCells);
        }
    }
}
