using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterPatternDesigin
{
    internal class AndCriteria : ICriteria
    {
        private  ICriteria _criteria;
        private ICriteria _otherCriteria;

        public AndCriteria(ICriteria criteria, ICriteria otherCriteria)
        {
            _criteria = criteria;
            _otherCriteria = otherCriteria;
        }

        public List<Person> MeetCriteria(List<Person> persons)
        {
            var criteriaFilterItme=_criteria.MeetCriteria(persons);
            var otherFilterItems=_otherCriteria.MeetCriteria(criteriaFilterItme);
            return otherFilterItems;            
        }
    }
}
