using System;
using System.Collections;
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
            var sequence = GetSequenceFromConsole();
            var ordered = sequence.OrderByDescending(s => s.Length);
                                  //.OrderBy(s => s.Length)
                                  //.ThenBy(s => s);

            foreach (var item in ordered)
            {
                Console.WriteLine(item);
            }
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
