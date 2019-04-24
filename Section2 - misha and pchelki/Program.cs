using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section2___misha_and_pchelki
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Storage<int> stor = new Storage<int>(5))
            {
                Reader<int> reader = new Reader<int>(stor);
                Writer<int> writer1 = new Writer<int>(stor, 1, 1);
                Writer<int> writer2 = new Writer<int>(stor, 2, 2);
                Writer<int> writer3 = new Writer<int>(stor, 3, 3);
                Console.Read();
            }
                
        }
    }
}
