using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Section2___misha_and_pchelki
{
    public class Reader<T>
    {
        public Reader(IStorage<T> stor)
        {
            var task = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    stor.Dequeue();
                    Thread.Sleep(5000);
                }
            });
        }
    }
}
