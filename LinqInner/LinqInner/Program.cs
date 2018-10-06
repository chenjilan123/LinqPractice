using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqInner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
        static IEnumerable<string> GetSequenceFromConsole()
        {
            var input = string.Empty;
            input = Console.ReadLine();
            while (input != "end!")
            {
                yield return input;
                input = Console.ReadLine();
            }
        }
    }
}
