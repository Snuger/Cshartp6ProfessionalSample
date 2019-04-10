using CrazyElemphant.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyElemphant.Client.Contracts
{
    public interface IDishRespostry:IQueryRepostry<Dish,int>,IUpdateRepostry<Dish,int>
    {
    }
}
