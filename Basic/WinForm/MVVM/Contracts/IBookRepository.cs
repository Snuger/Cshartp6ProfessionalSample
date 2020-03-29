using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IBookRepository:IQueryRepository<Book,int>,IUpdateRepository<Book,int>
    {

    }
}
