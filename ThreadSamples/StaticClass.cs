using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSamples
{
    internal static class StaticClass
    {
        static bool IsExectued;
        /// <summary>
        /// Local variables in a method are not shared across threads
        /// static method doesnt matter here, as long as it is uses local variables
        /// here IsExecuted is bool type, so values are passed by value, basically it is a local variable.
        /// </summary>
        /// <param name="IsExectued"></param>
        public static void StaticMethod_WithPassByValueVariable(bool IsExectued)
        {

            if (!IsExectued)
            {
                IsExectued = true;
                int cycleCount = 5;
                for (int cycles = 0; cycles < cycleCount; cycles++) 
                    Console.WriteLine("Executed " + cycles);
            }
        }

        /// <summary>
        /// Local variables in a method are not shared across threads
        /// static method doesnt matter here, as long as it is uses local variables
        /// here flag is reference type, so values are passed by reference, basically it is a shared variable and not a local          variable
        /// </summary>
        /// <param name="flag"></param>
        public static void StaticMethod_WithPassByReferenceVariable(Flag flag)
        {

            Console.WriteLine("Thread Id called: " + Thread.CurrentThread.ManagedThreadId);
            if (flag.IsExectued)
            {
                flag.IsExectued = false;
                int cycleCount = 5;
                for (int cycles = 0; cycles < cycleCount; cycles++) 
                    Console.WriteLine("Executed " + cycles);
                Console.WriteLine("Thread id executed inside loop: " + Thread.CurrentThread.ManagedThreadId);
            }
        }

        /// <summary>
        /// Static variable in the method are shared across all threads
        /// here IsExecuted is a static variable which is shared across all threads.
        /// </summary>
        /// <param name="IsExectued"></param>
        public static void StaticMethod_WithStaticVariable()
        {
            if (!IsExectued)
            {
                IsExectued = true;
                int cycleCount = 5;
                for (int cycles = 0; cycles < cycleCount; cycles++) 
                    Console.WriteLine("Executed " + cycles);
            }
        }
    }

    internal class Flag
    {
        public bool IsExectued { get; set; }
    }
}
