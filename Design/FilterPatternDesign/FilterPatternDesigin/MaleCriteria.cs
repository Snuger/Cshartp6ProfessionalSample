using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterPatternDesigin
{
    internal class MaleCriteria : ICriteria
    {
        public List<Person> MeetCriteria(List<Person> persons)=>  persons.Where(u=>u.Gender.Equals("male",StringComparison.OrdinalIgnoreCase)).ToList();     
    }
}
