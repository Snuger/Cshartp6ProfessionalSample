using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterPatternDesigin
{
    internal class OrCriteria : ICriteria
    {
        private readonly ICriteria _criteria;
        private readonly ICriteria _otherCriteria;

        public OrCriteria(ICriteria criteria, ICriteria otherCriteria)
        {
            _criteria = criteria;
            _otherCriteria = otherCriteria;
        }

        public List<Person> MeetCriteria(List<Person> persons)
        {
            var criteriaFilterItmes=_criteria.MeetCriteria(persons).ToList();
            var otherCriterFilterItems=_otherCriteria.MeetCriteria(persons).ToList();
            if (criteriaFilterItmes == null)
                return otherCriterFilterItems;           
            criteriaFilterItmes.AddRange(otherCriterFilterItems);
            return criteriaFilterItmes;
        }
    }
}
