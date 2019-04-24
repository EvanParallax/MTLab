using System;
using System.Threading;

namespace Lab4
{
    class CMyBarrier : IMyBarrier
    {
        private readonly ManualResetEvent manualEvent;

        private readonly object sync;

        private int maxCount;

        public int ParticipantCount { get; private set; }

        public CMyBarrier(int participantCount)
        {
            if (participantCount <= 0)
            {
                throw new ArgumentException(nameof(participantCount));
            }

            maxCount = participantCount;
            ParticipantCount = participantCount;
            manualEvent = new ManualResetEvent(false);
            sync = new object();
        }

        public bool SignalAndWait(TimeSpan timeout)
        {
            if (timeout <= TimeSpan.Zero)
            {
                throw new ArgumentException(nameof(timeout));
            }

            bool result = true;
            bool countState;

            lock (sync)
            {
                if (ParticipantCount == 0)
                {
                    throw new MyBarrierException();
                }
                ParticipantCount--;

                countState = ParticipantCount == 0;
            }

                if (countState)
                    manualEvent.Set();
                else
                    result = manualEvent.WaitOne(timeout);

                lock (sync)
                {
                    ParticipantCount++;
                    if (maxCount == ParticipantCount)
                        manualEvent.Reset();
                }
            return result;
        }

        public void Dispose()
        {
            manualEvent.Dispose();
        }
    }
}