using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelTaskWithLimit
{
    class Program
    {
        static List<Task> tasks = new List<Task>();
        static void Main(string[] args)
        {
            Console.WriteLine("Parallel Tasks with Limit...");
            
            int messagesCount = 0;
            int i = 0;
            while (true)
            {
                if (tasks.Count < 10)
                {
                    DoWorkInNewTask(messagesCount);
                    //tasks.Add(task);
                    messagesCount++;
                }
                if (messagesCount >= 50)
                    break;

            }
            
            Console.ReadLine();

        }

        static async Task DoWorkInNewTask(int i)
        {
            var task =Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Work {i} : {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(5000);
               
            });
            tasks.Add(task);
            await task;
            Console.WriteLine($"Work Completed: {i}");
            tasks.Remove(task);

        }
    }
}
