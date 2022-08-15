using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsyncDB
{
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
            using (var sql = new SqlConnection(""))
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

        public Product GetDataById(int id)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            using (var sql = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=Inventory;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                sql.Open();
                string query = "Select id,Name from Product where id= @id";
                SqlCommand cmd = new SqlCommand(query,sql);
                cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var product = new Product()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                    sw.Stop();
                    Console.WriteLine(sw.ElapsedMilliseconds);
                    return product;
                }
                return new Product();
            }
        }

        public Product GetDataByName(string name)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            using (var sql = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=Inventory;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                sql.Open();
                string query = "Select id, Name from Product where Name= @name";
                SqlCommand cmd = new SqlCommand(query, sql);
                cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value = name;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var product = new Product()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                    sw.Stop();
                   // Console.WriteLine(sw.ElapsedMilliseconds);
                    return product;
                }
                return new Product();
            }
        }

    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
