using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1___provider_and_consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            IStorage<int> stor = new Storage<int>(5);

            var readtask1 = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    stor.Dequeue();
                    Thread.Sleep(1000);
                }
            });

            var readtask2 = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    stor.Dequeue();
                    Thread.Sleep(1000);
                }
            });

            var writetask1 = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    stor.Enqueue(1);
                    Thread.Sleep(1000);
                }
            });

            var writetask2 = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    stor.Enqueue(2);
                    Thread.Sleep(1000);
                }
            });
            Console.Read();
        }
    }
}
