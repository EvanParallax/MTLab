using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BearAndBees
{
    public class Bee<T>
    { 
        private static readonly object sync = new object();

        public Bee(IStorage<T> stor, IAwakable bear, int id, T obj)
        {
            var task = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    stor.Enqueue(obj);
                    lock (sync)
                    {
                        if (stor.IsFull())
                            bear.Awake();
                    }

                    Thread.Sleep(1000);
                }
            });
        }
    }
}
