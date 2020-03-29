using System;
using System.Collections.Generic;
using System.Text;

namespace DocumnetManager.Constructor
{
    public class Fund<TFund>
        where TFund: new()
    {
        public Fund()
        {
            EnterpriseFund = new TFund();
        }

        TFund EnterpriseFund { get; set; }
    }
}
