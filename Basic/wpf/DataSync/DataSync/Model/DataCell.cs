using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSync.Model
{
   public  class DataCell
    {

        public int Id { get; set; }
        
        public string  Name { get; set; }

        public int  Total { get; set; }

        public int  UploadedTotal { get; set; }

        public string BusinessDate { get; set; }


        private bool complated;

        public bool Complated
        {
            get { return complated; }
            set => complated=(UploadedTotal >= Total) ? true : value;           
        }

    }
}
