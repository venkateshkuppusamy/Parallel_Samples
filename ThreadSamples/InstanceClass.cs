using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSamples
{
    internal class InstanceClass
    {
        private bool IsExectued;

        private static bool staticIsExecuted;
        private static readonly Object locker = new Object();

        /// <summary>
        /// In the below example Instance method uses a instance variable, so its instance variables are not shared until the instance itself is shared
        /// </summary>
        public void InstanceMethod()
        {
            if (!this.IsExectued) { 
                this.IsExectued = true; 
                Console.WriteLine($"{nameof(InstanceMethod)} Executed"); 
            }
        }

        /// <summary>
        /// In the below example Instance method uses a static variable which is shared across all threads even though seperate instances are given to each thread.
        /// </summary>
        public void InstanceMethod_WithStaticResourceAccess()
        {
            if (!staticIsExecuted) { 
                staticIsExecuted = true; 
                Console.WriteLine($"{nameof(InstanceMethod_WithStaticResourceAccess)} Executed"); 
            }
        }

        /// <summary>
        /// In the below example Instance method uses a static variable which is shared across all threads even though seperate instances are given to each thread. Access to the static resource is thread safe.
        /// </summary>
        public void InstanceMethod_WithStaticResourceAccess_ThreadSafe()
        {
            lock (locker)
            {
                if (!staticIsExecuted) { 
                    staticIsExecuted = true; 
                    Console.WriteLine($"{nameof(InstanceMethod_WithStaticResourceAccess_ThreadSafe)} Executed");
                }
            }
        }

    }
}
