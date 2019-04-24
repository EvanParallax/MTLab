using System;
using System.Collections.Generic;
using System.Threading;

namespace BearAndBees
{
    public interface IStorage<T> : IDisposable
    {
        bool Check();
        void Enqueue(T obj);
        List<T> Dequeue();
    }

    public class Storage<T> : IStorage<T>
    {

        private readonly ManualResetEvent writeEvent;

        private readonly int maxCapacity;

        private readonly Queue<T> queue;

        public Storage(int capacity)
        {
            maxCapacity = capacity;
            queue = new Queue<T>();
            writeEvent = new ManualResetEvent(true);
        }

        public bool Check()
        {
            lock (queue)
                return queue.Count >= maxCapacity;
        }

        public void Dispose()
        {
            writeEvent.Dispose();
        }

        public void Enqueue(T obj)
        {
            writeEvent.WaitOne();

            Thread.Sleep(1000);

            lock (queue)
            {
                if (queue.Count + 1 >= maxCapacity)
                {
                    writeEvent.Reset();
                }
                Console.WriteLine("pushing honey");
                queue.Enqueue(obj);
            }
            Thread.Sleep(1000);
        }

        public List<T> Dequeue()
        {
            var buff = new List<T>();
            lock (queue)
            {
                Console.WriteLine("start eating honey ");
                while (queue.Count != 0)
                {
                    buff.Add(queue.Dequeue());
                    Console.WriteLine("eating honey ");
                }

                writeEvent.Set();
                Console.WriteLine("end eating honey ");
                return buff;
            }
        }
    }
}
