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
            while (input != "end!")
            {
                input = Console.ReadLine();
                yield return input;
            }
        }

    }

    static class LinqExtension
    {
        public static TAggregate Aggregate<TAggregate, TSource>(this IEnumerable<TSource> source, Func<TSource, TAggregate, TAggregate> func)
        {
            return default(TAggregate);
        }
    }
}
