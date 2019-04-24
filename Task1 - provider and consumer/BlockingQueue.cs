using System;
using System.Collections.Generic;
using System.Threading;

namespace Task1___provider_and_consumer
{
    public interface IBlockingQueue<T>
    {
        void Enqueue(T obj);
        T Dequeue();
    }

    public sealed class BlockingQueue<T> : IBlockingQueue<T>
    {
        private readonly Queue<T> m_queue = new Queue<T>();
        private readonly Mutex m_mutex = new Mutex();
        private readonly Semaphore m_producerSemaphore;
        private readonly Semaphore m_consumerSemaphore;

        public BlockingQueue(int capacity)
        {
            m_producerSemaphore = new Semaphore(capacity, capacity);
            m_consumerSemaphore = new Semaphore(0, capacity);
        }

        public void Dispose()
        {
            m_producerSemaphore.Dispose();
            m_consumerSemaphore.Dispose();
        }

        public void Enqueue(T obj)
        {
            m_producerSemaphore.WaitOne();

            m_mutex.WaitOne();
            try
            {
                m_queue.Enqueue(obj);
                Console.WriteLine("Enqueue" + obj);
            }
            finally
            {
                m_mutex.ReleaseMutex();
            }
            m_consumerSemaphore.Release();
        }

        public T Dequeue()
        {

            m_consumerSemaphore.WaitOne();


            T value;
            m_mutex.WaitOne();
            try
            {
                value = m_queue.Dequeue();
                Console.WriteLine("Dequeue" + value);
            }
            finally
            {
                m_mutex.ReleaseMutex();
            }

            m_producerSemaphore.Release();
            return value;
        }
    }
}
