using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api_Test
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var watch = new Stopwatch();
            var apiCalls = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                apiCalls.Add(i);
            }

            watch.Start();
            await Task.WhenAll(apiCalls.Select(x => MakeApiCall(x)));
            watch.Stop();

            System.Console.WriteLine($"Time elapsed : {watch.Elapsed}");
        }

        public static async Task MakeApiCall(int number)
        {
            var client = new HttpClient();
            var responce = await client.GetAsync("https://localhost:5001/api/values");
            // System.Console.WriteLine($"Api call: {number}");
        }
    }
}
