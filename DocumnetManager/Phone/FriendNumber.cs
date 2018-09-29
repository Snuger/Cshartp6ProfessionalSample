using System;
using System.Collections.Generic;
using System.Text;

namespace DocumnetManager.Phone
{
   public  class FriendNumber:PhoneNumber
    {
        private bool isWorkerNumber;

        public FriendNumber(string number, string name,bool isWorkerNumber) : base(number, name)
        {
            IsWorkerNumber = isWorkerNumber;
        }

        public bool IsWorkerNumber { get => isWorkerNumber; set => isWorkerNumber = value; }





    }
}
