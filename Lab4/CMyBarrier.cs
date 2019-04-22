using System;
using System.Threading;

namespace Lab4
{
    class CMyBarrier : IMyBarrier
    {

        private object sync;

        public int ParticipantCount { get; private set; }

        public CMyBarrier(int participantCount)
        {
            if (participantCount <= 0)
            {
                throw new ArgumentException(nameof(participantCount));
            }

            ParticipantCount = participantCount;
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

            Timer timer = null;
            try
            {
                var callback = new TimerCallback((p) => result = false);

                timer = new Timer(callback, 0, 0, (int)timeout.TotalMilliseconds);

                lock (sync)
                {
                    ParticipantCount--;

                    if (ParticipantCount == 0)
                        Monitor.PulseAll(sync);
                    else
                        Monitor.Wait(sync);
                }
                //lock (sync)
                //{ 
                //    ParticipantCount++;
                //}
            }
            finally
            {
               timer?.Dispose();
            }

            return result;
        }

        public void Dispose()
        {

        }
    }
}