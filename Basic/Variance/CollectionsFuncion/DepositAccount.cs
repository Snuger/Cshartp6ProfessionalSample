using System;
using System.Collections.Generic;
using System.Text;

namespace Variance.CollectionsFuncion
{
    public class DepositAccount : IAccount
    {
        public DepositAccount(decimal balance, string name)
        {
            Balance = balance;
            Name = name;
        }

        public decimal Balance{get;set;}

        public string Name { get; }
    }
}
