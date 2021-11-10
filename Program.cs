using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebAPIPractice
{
    class Program
    {

        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.boredapi.com/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                Activity activity = null;
                HttpResponseMessage response = await client.GetAsync("activity");

                if (response.IsSuccessStatusCode)
                {
                    activity = await response.Content.ReadAsAsync<Activity>();
                    Console.WriteLine(activity.ToString());
                }
                else
                {
                    throw new HttpRequestException($"Unable to get activity.");
                }
            }
            catch (HttpRequestException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

        }

        class Activity
        {
            public string Key { get; set; }
            public string ActivityName { get; set; }
            public float Price { get; set; }

            public Activity(string key, string activity, float price)
            {
                Key = key;
                ActivityName = activity;
                Price = price * 100;
            }

            public override string ToString()
            {
                return $"{ActivityName}. It costs ${Price}. You should try it out!";
            }
        }

    }
}
