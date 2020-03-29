﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApplicationSample.Services
{
    public class DefaultSampleService:ISampleService
    {
        private List<string> _strings = new List<string> { "one", "tow", "three" };

        public IEnumerable<string> GetSampleStrings() => _strings;
    }
}
