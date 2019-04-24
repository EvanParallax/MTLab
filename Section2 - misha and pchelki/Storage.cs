using System;
using System.Collections.Generic;
using System.Threading;

namespace Section2___misha_and_pchelki
{
    public interface IStorage<T> : IDisposable
    {
        void Enqueue(T obj);
        List<T> Dequeue();
    }

    public class Storage<T> : IStorage<T>
    {
        private readonly ManualResetEventSlim readEvent;

        private readonly ManualResetEventSlim writeEvent;

        private readonly int maxCapacity;

        private readonly Queue<T> queue;

        public Storage(int capacity)
        {
            maxCapacity = capacity;
            queue = new Queue<T>();
            readEvent = new ManualResetEventSlim(false);
            writeEvent = new ManualResetEventSlim(true);
        }

        public void Dispose()
        {
            readEvent.Dispose();
            writeEvent.Dispose();
        }

        public void Enqueue(T obj)
        {
            writeEvent.Wait();

            lock (queue)
            {
                if (queue.Count + 1 == maxCapacity)
                {
                    readEvent.Set();
                    writeEvent.Reset();
                }
                Console.WriteLine("start writing to queue ");
                queue.Enqueue(obj);
                Console.WriteLine("end writing to queue ");
            }
        }

        public List<T> Dequeue()
        {
            readEvent.Wait();
            var buff = new List<T>();
            lock (queue)
            {
                Console.WriteLine("start reading from queue ");
                while (queue.Count != 0)
                {
                    buff.Add(queue.Dequeue());
                    Console.WriteLine("reading from queue ");
                }

                writeEvent.Set();
                readEvent.Reset();
                Console.WriteLine("end reading from queue ");
                return buff;
            }
        }
    }
}
