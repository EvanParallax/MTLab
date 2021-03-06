﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lab4
{
    class BarrierReturnTrueWhenWaitingThreadsCountEqualsParticipantCount : IDisposable
    {
        private TimeSpan barrierTimeSpan = TimeSpan.FromSeconds(1);
        private const int barrierParticipantCount = 20;
        private IMyBarrier myBarrier;

        public BarrierReturnTrueWhenWaitingThreadsCountEqualsParticipantCount()
        {
            myBarrier = new CMyBarrier(barrierParticipantCount);
        }

        void SignalBarrier()
        {
            var result = myBarrier.SignalAndWait(barrierTimeSpan);
            if (result)
            {
                Console.WriteLine("SignalAndWait result is true: Barrier is full");
            }
            else
            {
                Console.WriteLine("SignalAndWait result is false: timeout is ducked up");
            }
        }

        public void Execute()
        {
            var list = new List<Task>();
            for (int i = 0; i < barrierParticipantCount; i++)
            {
                int id = i;
                var task = new Task(() =>
                    {
                        Console.WriteLine("Task " + id + " before barrier");
                        SignalBarrier();
                        Console.WriteLine("Task " + id + " after barrier");
                    }
                );
                task.Start();
                list.Add(task);
            }

            Task.WaitAll(list.ToArray());
        }

        public void Dispose()
        {
            myBarrier?.Dispose();
        }
    }
}