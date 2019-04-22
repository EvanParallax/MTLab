using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lab4
{
    class ThrowExceptionAfterOverParticipantCount : IDisposable
    {
        private TimeSpan _barrierTimeSpan = TimeSpan.FromSeconds(10);
        private const int BarrierParticipantCount = 2;
        private IMyBarrier _myBarrier;

        public ThrowExceptionAfterOverParticipantCount()
        {
            _myBarrier = new CMyBarrier(BarrierParticipantCount);
        }

        void SignalBarrier()
        {
            Console.WriteLine();
            
                _myBarrier.SignalAndWait(_barrierTimeSpan);
            
            
        }

        public void Execute()
        {
            var list = new List<Task>();
            for (int i = 0; i < 3; i++)
            {
                var task = Task.Run(() => SignalBarrier());
                list.Add(task);
            }

            Console.Read();
        }

        public void Dispose()
        {
            _myBarrier?.Dispose();
        }
    }
}