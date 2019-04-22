using System;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var test1 = new ThrowExceptionAfterOverParticipantCount())
            {
                test1.Execute();
            }

            using (var test2 = new BarrierReturnTrueWhenWaitingThreadsCountEqualsParticipantCount())
            {
                test2.Execute();
            }

            Console.ReadKey();
        }
    }
}