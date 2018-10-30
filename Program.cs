using System;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace SpanBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            for (long i = 1000000; i < 1000000000; i*= 10)
            {
                SpanLoop(i);
                ArrayLoop(i);
            }

            Console.WriteLine($"Loops Done.");

            for (long i = 1000000; i < 1000000000; i*= 10)
            {
                SpanSeek(i);
                ArraySeek(i);
            }

            Console.WriteLine($"Seeks Done.");

            Console.ReadKey();
        }

        static void SpanLoop(long count)
        {
            Span<byte> span = stackalloc byte[4096];

            Stopwatch s = new Stopwatch();
            s.Start();

            int pos = 0;

            for (int i = 0; i < count; i++)
            {
                pos = (pos + 4) % span.Length - 4;
                byte x = span[pos];
                span[pos] = x;
            }

            s.Stop();
            Console.WriteLine($"Span \t{count}\t{s.ElapsedMilliseconds}ms");
        }

        static void ArrayLoop(long count)
        {
            var array = new byte[4096];

            Stopwatch s = new Stopwatch();
            s.Start();

            int pos = 0;

            for (int i = 0; i < count; i++)
            {
                pos = (pos + 4) % array.Length - 4;
                byte x = array[pos];
                array[pos] = x;
            }

            s.Stop();
            Console.WriteLine($"Array \t{count}\t{s.ElapsedMilliseconds}ms");
        }

        static void SpanSeek(long count) {
            Span<byte> span = new byte[4096];

            int start = new Random().Next(4095);
            int length = new Random().Next(start, 4095) - start;

            Stopwatch s = new Stopwatch();
            s.Start();

            for (int i = 0; i < count; i++)
            {
                var bytes = span.Slice(start, length);
            }

            s.Stop();
            Console.WriteLine($"Span seek \t{count}\t{s.ElapsedMilliseconds}ms");
        }

        static void ArraySeek(long count) {
            var array = new byte[4096];

            int start = new Random().Next(4095);
            int length = new Random().Next(start, 4095) - start;

            Stopwatch s = new Stopwatch();
            s.Start();

            for (int i = 0; i < count; i++)
            {
                var bytes = array.Skip(start).Take(length);
            }

            s.Stop();
            Console.WriteLine($"Array seek \t{count}\t{s.ElapsedMilliseconds}ms");
        }
    }
}
