using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSamples.Tasks
{
    internal class BasicTaskOperation
    {

        public void Run()
        {
            Task.Run(() => { int i = 10; Exectue(i); });
            Console.WriteLine("Main Thread Executed in " + Thread.CurrentThread.ManagedThreadId);

            Task.Factory.StartNew(() => { int i = 10; Exectue(i); });

            var task = WrapExecuteInTask(10);
            task.Start();
        }

        public void Exectue(int counter)
        {
            for (int i = 0; i < counter; i++)
            {
               Task.Delay(1000);
            }
            Console.WriteLine("Returning Execute Function from " + Thread.CurrentThread.ManagedThreadId);
        }

        public Task WrapExecuteInTask(int counter)
        {
            return new Task(() => { int i = counter; Exectue(i); });
        }
    }
}
