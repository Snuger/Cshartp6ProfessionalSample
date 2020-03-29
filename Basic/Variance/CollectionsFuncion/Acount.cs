using System;
using System.Collections.Generic;
using System.Text;

namespace Variance.CollectionsFuncion
{
    public class Account:IAccount
    {     
        public Account(string name, decimal balance)
        {
            Name = name;
            Balance = balance;
        }

        public string Name { get; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; private set;}

    }
}
