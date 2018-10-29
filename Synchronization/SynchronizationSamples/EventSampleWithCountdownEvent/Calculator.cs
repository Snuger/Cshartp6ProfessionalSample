﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventSampleWithCountdownEvent
{
    public class Calculator
    {
        private CountdownEvent _mEvent;

        public int Result { get; private set; }


        public Calculator(CountdownEvent resetEvent)
        {
            _mEvent = resetEvent;
        }

        public void Calulation(int x, int y)
        {
            Console.WriteLine($"task {Task.CurrentId} starts Calulation");
            Task.Delay(new Random().Next(3000)).Wait();
            Result = x + y;
            Console.WriteLine($"Task {Task.CurrentId} is ready");

            _mEvent.Signal();
        }
    }
}