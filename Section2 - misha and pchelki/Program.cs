using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BearAndBees
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Storage<int> stor = new Storage<int>(5))
            {
                Bear<int> bear = new Bear<int>(stor);
                Bee<int> writer1 = new Bee<int>(stor, bear,1, 1);
                Bee<int> writer2 = new Bee<int>(stor, bear, 2, 2);

                Thread.Sleep(5000);

                Bee<int> writer3 = new Bee<int>(stor, bear,3, 3);
                Console.Read();
            }
                
        }
    }
}
