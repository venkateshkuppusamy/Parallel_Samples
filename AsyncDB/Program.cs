using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDB
{
    class Program
    {
        static void Main(string[] args)
        {
            long executionTime = 0;

            executionTime = Measure(SequentialDBCall);
            Console.WriteLine(nameof(SequentialDBCall) + " " + executionTime);

            executionTime = Measure(ParallelForDbCall);
            Console.WriteLine(nameof(ParallelForDbCall) + " " + executionTime);

            executionTime = Measure(TaskRunDbCall);
            Console.WriteLine(nameof(TaskRunDbCall) + " " + executionTime);
            
            executionTime = Measure(ParallelThreadDbCall);
            Console.WriteLine(nameof(ParallelThreadDbCall) + " " + executionTime);

        }

        private static void ParallelThreadDbCall()
        {
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < 100; i++)
            {
                DBLayer dB = new DBLayer();

                var thread = new Thread(() => dB.GetDataByName("Name - 100000"));
                thread.Start();
                threads.Add(thread);
            }
            threads.ForEach(e => e.Join());
        }

        private static void TaskRunDbCall()
        {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 100; i++)
            {
                tasks.Add(
                Task.Run(() =>
                {
                    DBLayer dB = new DBLayer();
                    var product = dB.GetDataByName("Name - 100000");
                    Console.WriteLine(product.Id + product.Name);
                }));
            }

            Task.WaitAll(tasks.ToArray());
        }

        private static void ParallelForDbCall()
        {
            Parallel.For(0, 500, number =>
            {
                DBLayer dB = new DBLayer();
                var product = dB.GetDataByName("Name - 100000");
                //Console.WriteLine(product.Id + product.Name);
            });
        }

        private static void SequentialDBCall()
        {
            DBLayer dB = new DBLayer();
            for (int i = 0; i < 10; i++)
            {
                var product = dB.GetDataByName("Name - 100000");
                //Console.WriteLine(product.Id + product.Name);
            }
        }

        public static long Measure(Action action)
        {
            Stopwatch sw = Stopwatch.StartNew();
            action();
            return sw.ElapsedMilliseconds;
        }
    }
}
