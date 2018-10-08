using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionSample
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple =true,Inherited =true)]
    public class Verification:Attribute
    {
        private readonly DateTime _dateModified;
        private readonly string _changes;

        public Verification(DateTime dateModified, string changes)
        {
            _dateModified = dateModified;
            _changes = changes;
        }

        public string Disprition { get; set; }

        public string DataType { get; set; }

        public int Length { get; set; }

        public bool Required { get; set; }


        public DateTime DateModified => _dateModified;

        public string Changes => _changes;
    }
}
