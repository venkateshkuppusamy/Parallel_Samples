using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
         
            int i = 0;
            while (i < 10)
            {
                p.GetCustomer(i);
                i++;
            }
            Console.WriteLine("Main thread completed");
            Console.ReadLine();
        }

        async Task<int> GetCustomer(int i)
        {
            Console.WriteLine("Getting CustomerDetails: "+ i);
            Task<int> t =  GetCustomerCountFromDB(i);
            return await t;
            //Console.WriteLine($"Customer {i} count " + t.GetAwaiter().GetResult());
            
        }

        async Task<int> GetCustomerCountFromDB(int i)
        {
            int count = 0;
            Console.WriteLine($"Getting Customer {i} from db");
            Task t= Task.Run(() => { Thread.Sleep(3000); Console.WriteLine($"Get from DB complete for customer {i} "); count = 20; });
            Console.WriteLine("Made a db call asynchoronously");
            await t;
            return count;
        }
    }
}
