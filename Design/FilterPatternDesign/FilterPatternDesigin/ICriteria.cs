using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterPatternDesigin
{
    internal interface ICriteria
    {
        public List<Person> MeetCriteria(List<Person> persons);
    }
}
