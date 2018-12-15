using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CI_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CI Parallel tasking...");
            CIProcessor cIProcessor = new CIProcessor();
            cIProcessor.Process();
        }
    }
    class CIProcessor
    {
        Dictionary<int, bool> dic = new Dictionary<int, bool>();
        public void Process()
        {

            int i = 1;
            Random rand = new Random();
            int counter = 0;
            while (i < 11)
            {
                i = rand.Next(12);

                if (dic.ContainsKey(i))
                {
                    Console.WriteLine("Add this event to queue for later processing..." + i );
                }
                else
                {
                   // Console.WriteLine("Process event" + i);
                    dic.Add(i, true);
                    Task ts = new Task(CreateAttachEventToCycle, i);
                    ts.Start();
                }
                counter++;
            }
            Console.WriteLine($"Total Events processed" + counter);
            Console.ReadLine();
        }
        

        void CreateAttachEventToCycle(object e)
        {
            Thread.Sleep(10);
            Console.WriteLine("Processing Event" + (int) e);
            dic.Remove((int)e);
        }
    }
}
