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
            //Console.WriteLine(nameof(SequentialDBCall) + " " + executionTime);

            //executionTime = Measure(ParallelForDbCall);
            //Console.WriteLine(nameof(ParallelForDbCall) + " " + executionTime);

            //executionTime = Measure(TaskRunDbCall);
            //Console.WriteLine(nameof(TaskRunDbCall) + " " + executionTime);

            //executionTime = Measure(ParallelThreadDbCall);
            //Console.WriteLine(nameof(ParallelThreadDbCall) + " " + executionTime);


            //executionTime = Measure(SequentialDBCallAsync);
            //Console.WriteLine(nameof(SequentialDBCallAsync) + " " + executionTime);

            //var executionTime2 = Measure(SequentialDBCallAsync);
            //Console.WriteLine(nameof(SequentialDBCallAsync) + " " + executionTime2);

            //var executionTime3 = Measure(SequentialDBCallAsync);
            //Console.WriteLine(nameof(SequentialDBCallAsync) + " " + executionTime3);

            //var executionTime4 = Measure(SequentialDBCallAsync);
            //Console.WriteLine(nameof(SequentialDBCallAsync) + " " + executionTime4);

            //executionTime = Measure(ParallelForDbCallAsync);
            //Console.WriteLine(nameof(ParallelForDbCallAsync) + " " + executionTime);

            //executionTime = Measure(TaskRunDbCallAsync);
            //Console.WriteLine(nameof(TaskRunDbCallAsync) + " " + executionTime);


            //executionTime = Measure(ParallelThreadDbCallAsync);
            //Console.WriteLine(nameof(ParallelThreadDbCallAsync) + " " + executionTime);
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

        private static void ParallelThreadDbCallAsync()
        {
            List<Thread> threads = new List<Thread>();
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 10000; i++)
            {
                DBLayer dB = new DBLayer();

                var thread = new Thread(() => tasks.Add(dB.GetDataByNameAsync("Name - 100000")));
                thread.Start();
                threads.Add(thread);
            }
            Console.WriteLine(Process.GetCurrentProcess().Threads.Count);
            Task.WaitAll(tasks.ToArray());
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

        private static void TaskRunDbCallAsync()
        {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 100; i++)
            {
                tasks.Add(
                Task.Run(() =>
                {
                    DBLayer dB = new DBLayer();
                    var product = dB.GetDataByNameAsync("Name - 100000");
                    tasks.Add(product);
                    //Console.WriteLine(product.Id + product.Name);
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

        private static void ParallelForDbCallAsync()
        {
            List<Task> tasks = new List<Task>();
            Parallel.For(0, 100, number =>
            {
                DBLayer dB = new DBLayer();
                var product = dB.GetDataByNameAsync("Name - 100000");
                tasks.Add(product);
            });

            Task.WaitAll(tasks.ToArray());
        }


        private static void SequentialDBCall()
        {
            DBLayer dB = new DBLayer();
            for (int i = 0; i < 1; i++)
            {
                var product = dB.GetDataByName("Name - 100000");
                Console.WriteLine(i);
            }
        }

        private static void SequentialDBCallAsync()
        {
            List<string> productNames = new List<string>() { "Name - 100000", "Name - 100001", "Name - 100002", "Name - 10003", "Name - 10004" };
            Random rnd = new Random();
            DBLayer dB = new DBLayer();
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 500; i++)
            {
                var product = dB.GetDataByNameAsync(productNames[rnd.Next(0,4)]);
                tasks.Add(product);
               // Console.WriteLine(i);
            }
            Task.WaitAll(tasks.ToArray());
        }

        public static long Measure(Action action)
        {
            Stopwatch sw = Stopwatch.StartNew();
            action();
            return sw.ElapsedMilliseconds;
        }

        public static async Task<long> MeasureAsync(Func<Task> func)
        {
            Stopwatch sw = Stopwatch.StartNew();
            await func();
            return sw.ElapsedMilliseconds;
        }
    }
}
