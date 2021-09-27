using DataLayer;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PresentationLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            CallWebApiAsync().Wait();
            Console.ReadLine();
        }
        static async Task CallWebApiAsync()
        {
            using(var client=new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49986/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = await client.GetAsync("api/Brands");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsAsync<List<Brand>>();
                    foreach (var item in data)
                    {
                        Console.WriteLine("========Line========");
                        Console.WriteLine("Brand Name:" + item.BrandName);
                        Console.WriteLine("Brand Name:" + item.Description);
                        Console.WriteLine("========End========");
                    }
                }
                else
                {
                    Console.WriteLine("Something wrong!");
                }
            }

        }
    }
}
