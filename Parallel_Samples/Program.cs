using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel_Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main thread started...");
            Action ac = new Action(NewTask);
            Task ts = new Task(ac);
            Task tswithParam = new Task(TaskWithParam, new TaskModel() { TaskID = 1, Value = "Value1" });

            ts.Start();
            tswithParam.Start();

            Task t1 = new Task(MultipleTask);
            Task t2 = new Task(MultipleTask);
            Task t3 = new Task(MultipleTask);
            Task t4 = new Task(MultipleTask);

            t1.Start(); t2.Start(); t3.Start(); t4.Start();
            t1.Wait();
            
            Console.WriteLine("Main thread ended...");
            Console.ReadLine();
        }


        static void MultipleTask()
        {
            Thread.Sleep(2000);
            Console.WriteLine("One of multiple task getting executed" + new Random().Next(100));
        }
        static void NewTask() {
            Console.WriteLine("New Task executing");
        }

        static void TaskWithParam(object param)
        {
            TaskModel tm = (TaskModel)param;
            Console.WriteLine($"Task with Param executing with TaskID : {tm.TaskID }, Task Value: {tm.Value}");
        }
    }

    class TaskModel
    {
        public string Value { get; set; }
        public int TaskID { get; set; }
    }
    
}

