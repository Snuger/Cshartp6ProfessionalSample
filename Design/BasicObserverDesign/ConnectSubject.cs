using System;
using System.Collections.Generic;
using System.Text;

namespace BasicObserverDesign
{
    class ConnectSubject : Subject
    {
        private string subjectState;

        public string SubjectState
        {
            get { return subjectState; }
            set { subjectState = value; }
        }

    }
}
