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
            var sequence = GetSequenceFromConsole();
            var aggregate = sequence.Select(i => i).Aggregate<string, string>("Hello  ", (total, current) => total + ", " + current);
            Console.WriteLine(aggregate);

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

    static class LinqExtension
    {
        public static TAggregate Aggregate<TAggregate, TSource>(this IEnumerable<TSource> source,  TAggregate seed, Func<TAggregate, TSource, TAggregate> func)
        {
            var enumerator = source.GetEnumerator();
            enumerator.MoveNext();
            var aggregate = func(seed, enumerator.Current);
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                aggregate = func(aggregate, current);
            }
            return aggregate;
        }
    }
}
