using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Lab4
{
    class ThrowExceptionAfterOverParticipantCount : IDisposable
    {
        private TimeSpan barrierTimeSpan = TimeSpan.FromSeconds(10);
        private const int barrierParticipantCount = 2;
        private IMyBarrier myBarrier;

        public ThrowExceptionAfterOverParticipantCount()
        {
            myBarrier = new CMyBarrier(barrierParticipantCount);
        }

        void SignalBarrier()
        {
            Console.WriteLine();
            try
            {
                myBarrier.SignalAndWait(barrierTimeSpan);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Barrier count overflow");
            }

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
            myBarrier?.Dispose();
        }
    }
}