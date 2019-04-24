using System;
using System.Threading;

namespace Lab4
{
    class CMyBarrier : IMyBarrier
    {
        private readonly ManualResetEventSlim manualEvent;

        private readonly object sync;

        public int ParticipantCount { get; private set; }

        public CMyBarrier(int participantCount)
        {
            if (participantCount <= 0)
            {
                throw new ArgumentException(nameof(participantCount));
            }

            ParticipantCount = participantCount;
            manualEvent = new ManualResetEventSlim(false);
            sync = new object();
        }

        public bool SignalAndWait(TimeSpan timeout)
        {
            lock (sync)
            {
                if (ParticipantCount == 0)
                {
                    throw new MyBarrierException();
                }
            }

            if (timeout <= TimeSpan.Zero)
            {
                throw new ArgumentException(nameof(timeout));
            }

            bool result = true;
            bool countState;
                lock (sync)
                {
                    ParticipantCount--;

                    countState = ParticipantCount == 0;
                }

                if (countState)
                    manualEvent.Set();
                else
                    manualEvent.Wait(timeout);

            return result;
        }

        public void Dispose()
        {
            manualEvent.Dispose();
        }
    }
}