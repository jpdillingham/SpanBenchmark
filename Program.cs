using System;
using System.Diagnostics;
using System.Text;

namespace SpanBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            for (long i = 10000000; i < 10000000000000; i*= 10)
            {
                SpanLoop(i);
                ArrayLoop(i);
            }

            Console.WriteLine($"Done.");

            Console.ReadKey();
        }

        static void SpanLoop(long count)
        {
            var span = new ReadOnlySpan<byte>(Encoding.ASCII.GetBytes(new string(' ', 4096)));

            Stopwatch s = new Stopwatch();
            s.Start();

            int pos = 0;

            for (int i = 0; i < count; i++)
            {
                pos = (pos + 4) % span.Length - 4;
                int x = BitConverter.ToInt32(span.Slice(pos, 4));
            }

            s.Stop();
            Console.WriteLine($"Span \t{count}\t{s.ElapsedMilliseconds}ms");
        }

        static void ArrayLoop(long count)
        {
            var array = Encoding.ASCII.GetBytes(new string(' ', 4096));

            Stopwatch s = new Stopwatch();
            s.Start();

            int pos = 0;

            for (int i = 0; i < count; i++)
            {
                pos = (pos + 4) % array.Length - 4;
                int x = BitConverter.ToInt32(array, pos);
            }

            s.Stop();
            Console.WriteLine($"Array \t{count}\t{s.ElapsedMilliseconds}ms");
        }
    }
}
