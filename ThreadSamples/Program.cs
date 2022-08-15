using System.Diagnostics.Metrics;
using ThreadSamples;
using ThreadSamples.Tasks;

class Program
{
    bool done;
    static void Main()
    {
        //Thread t = new Thread(MethodY);          // Kick off a new thread
        //t.Start();                               // running MethodY()
        //// Simultaneously, do something on the main thread.
        //for (int i = 0; i < 1000; i++) Console.Write(Thread.CurrentThread.ManagedThreadId + "x ");

        //new Thread(() => StaticClass.StaticMethod_WithPassByValueVariable(false)).Start();
        //StaticClass.StaticMethod_WithPassByValueVariable(false);

        //Flag flag = new Flag() { IsExectued = true };
        //new Thread(() => StaticClass.StaticMethod_WithPassByReferenceVariable(flag)).Start();
        //StaticClass.StaticMethod_WithPassByReferenceVariable(flag);

        //new Thread(StaticClass.StaticMethod_WithStaticVariable).Start();
        //StaticClass.StaticMethod_WithStaticVariable();

        ///In the below example sharedObject is shared across both thread, so its instance variables are also shared
        ///
        //InstanceClass sharedObject = new InstanceClass();
        //new Thread(sharedObject.InstanceMethod).Start();
        //sharedObject.InstanceMethod();

        ///In the below example privateObject is used by new thread and privateObject2 is used in the main thread, so each thread has a copy of instance class object.
        ///
        //InstanceClass privateObject = new InstanceClass();
        //InstanceClass privateObject2 = new InstanceClass();
        //new Thread(privateObject.InstanceMethod).Start();
        //privateObject2.InstanceMethod();


        /// In the below example privateObject is used by new thread and privateObject2 is used by the main thread, however these object calls a methods which has access to a static variable. so that variable is shared across threads.
        ///
        //InstanceClass privateObject3 = new InstanceClass();
        //InstanceClass privateObject4 = new InstanceClass();
        //new Thread(privateObject3.InstanceMethod_WithStaticResourceAccess).Start();
        //privateObject4.InstanceMethod_WithStaticResourceAccess();

        //InstanceClass privateObject5 = new InstanceClass();
        //InstanceClass privateObject6 = new InstanceClass();
        //new Thread(privateObject5.InstanceMethod_WithStaticResourceAccess_ThreadSafe).Start();
        //privateObject6.InstanceMethod_WithStaticResourceAccess_ThreadSafe();

        //var thread = new Thread(Call);
        //thread.Start();
        //Thread.Sleep(25);
        //Console.WriteLine("code while child thead is executing");

        //thread.Join();
        //Console.WriteLine("Code after child thread is finised executed");

        new BasicTaskOperation().Run();
    }

    static void MethodY()
    {
        for (int i = 0; i < 100; i++) Console.Write(Thread.CurrentThread.ManagedThreadId + "y ");
    }

    static void Call()
    {
        for (int i = 0; i < 200; i++) Console.Write(Thread.CurrentThread.ManagedThreadId + "Call ");
    }

}