using System;
using System.Collections.Generic;

namespace Variance.CollectionsFuncion
{
    public class Algorithm
    {
        public static decimal AcclumateSample(IEnumerable<Account> acount)
        {
            decimal sum = 0;
            foreach (Account aco in acount)
            {
                sum += aco.Balance;
            }
            return sum;

        }

        public static decimal Acclumate<TAccount>(IEnumerable<TAccount> source) where TAccount : IAccount
        {
            decimal sum = 0;
            foreach (TAccount acc in source)
            {
                sum += acc.Balance;
            }
            return sum;
        }


        public static T2 Acclumate<T1, T2>(IEnumerable<T1> source, Func<T1, T2, T2> action)
        {
            T2 sum = default(T2);
            foreach (T1 item in source)
            {

                sum = action(item, sum);
            }
            return sum;
        }
    }
}
