using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1___provider_and_consumer
{
    public interface IStorage<T> : IDisposable
    {
        void Enqueue(T obj);
        T Dequeue();
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

        public T Dequeue()
        {
            readEvent.Wait();

            lock (queue)
            {
                Console.WriteLine("start reading from queue ");
                if (queue.Count == 1)
                {
                    readEvent.Reset();
                    writeEvent.Set();
                }
                Console.WriteLine("end reading from queue ");
                return queue.Dequeue();
            }
        }
    }
}
