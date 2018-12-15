using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsyncDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("async DB calls");
            DBLayer db = new DBLayer();
            db.Process();
            Console.ReadLine();
        }
    }

    class DBLayer
    {
        public void Process()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 100; i++)
            {
                GetDBData().ContinueWith(c=> { Console.WriteLine("Time : " + sw.ElapsedMilliseconds); });
                //GetDBData();
                Console.WriteLine($"{i} call to db");
                //for (int j = 0; j < 100; j++)
                //{
                //    Console.Write(j);
                //}
                
            }
            Console.WriteLine("Time : " + sw.ElapsedMilliseconds);
        }
        public async Task GetDBData()
        {
            using (var sql = new SqlConnection("data source=dev-cyclid-terra-ussc-cat-sql-01.database.windows.net,1433;initial catalog=devterracycliddb;user id=devuser;password=VGVycmE@MTIz"))
            {
                sql.Open();
                string query = "Insert into logs values('{\"log details\":\"ok\"}')";
                SqlCommand cmd = new SqlCommand(query, sql);
                var task = cmd.ExecuteReaderAsync();
                await task;
                var reader = task.GetAwaiter().GetResult();
                int i = 0;
                Console.WriteLine("Data fetched");
                while (reader.Read())
                {
                    //Console.WriteLine("Value: " + reader.GetValue(0));
                    i++;
                }
            }
        }
    }
}
