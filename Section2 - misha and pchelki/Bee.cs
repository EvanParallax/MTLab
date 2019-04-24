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
        private static object sync = new object();

        public Bee(IStorage<T> stor, IAwakable bear, int id, T obj)
        {
            var task = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    lock (sync)
                    {
                        stor.Enqueue(obj);
                        if (stor.Check())
                            bear.Awake();
                    }
                    Thread.Sleep(1000);
                }
            });
        }
    }
}
