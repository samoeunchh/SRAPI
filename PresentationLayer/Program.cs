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
        static readonly HttpClient client = new();
        static void Main(string[] args)
        {
            client.BaseAddress = new Uri("http://localhost:49986/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            //Call Post Data
            var brand = new Brand
            {
                BrandName = "Lexus",
                Description="Testing"
            };
            PostBrandAsync(brand).Wait();
            //Call Get Data
            CallWebApiAsync().Wait();
            Console.ReadLine();
        }
        static async Task CallWebApiAsync()
        {
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
        static async Task<bool> PostBrandAsync(Brand brand)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Brands",brand);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
