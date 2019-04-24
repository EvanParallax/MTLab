using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Section2___misha_and_pchelki
{
    public class Writer<T>
    {
        public Writer(IStorage<T> stor, int id, T obj)
        {
            var task = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    stor.Enqueue(obj);
                    Thread.Sleep(1000);
                }
            });
        }
    }
}
