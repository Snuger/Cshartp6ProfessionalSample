using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace EventSourceSamples
{
    [EventSource(Name ="wrox-sampleEvlentSource",Guid = "{DFA52D57-B8CE-4F2F-9112-986F872E1499}")]
    public class SampleEventSource:EventSource
    {
       public static SampleEventSource log = new SampleEventSource();

        [Event(1,ActivityOptions =EventActivityOptions.None,Channel =EventChannel.Operational,Keywords =EventKeywords.All,Level =EventLevel.Error,Message ="wqvfif",Opcode =EventOpcode.Start,Tags =EventTags.None,Task=EventTask.None,Version =1)]
        public void Startup() {
            //base.WriteEvent(1);
            SampleEventSource.log.WriteEvent(1);
        }

        public void ProcessStart(int x) {
            base.WriteEvent(1,x);
        }

        public void ProcessStop(int x) {
            base.WriteEvent(1, x);               
        }

        public void RequestBegin() {
            base.WriteEvent(4);
        }

        public void RequestEnd(int x) {
            base.WriteEvent(5);
        }
    }
}
