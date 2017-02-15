using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

using Newtonsoft.Json;

namespace GoodMorning
{
   
    public class NewsController
    {
        private const string URL = "https://newsapi.org/v1/articles";
        private string urlParameters = "?source=the-next-web&sortBy=latest&apiKey=8f180ef2fe0947fd9fd24e04bc920348";
        private HttpClient client;
        public NewsController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public List<News> GetData()
        {
            HttpResponseMessage response;
            try
            {
                response = client.GetAsync(urlParameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    string resp = response.Content.ReadAsStringAsync().Result;
                    return ParseJSONWithNews(resp);


                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    return null;
                }
            }
            catch(AggregateException e)
            {
                Console.WriteLine("Cannot get data from api");
                return null;
            }
           
        }

        private List<News> ParseJSONWithNews(string response)
        {
            string[] responseSplitted = response.Split('[');
            StringBuilder jsonwithNews = new StringBuilder();
            jsonwithNews.Append("[");//ading [ because it was in first part os responseSplitted
            jsonwithNews.Append(responseSplitted[1]);//second part from json with news
            jsonwithNews.Remove(jsonwithNews.Length - 1, 1);//removing last '}'
            List<News> json = JsonConvert.DeserializeObject<List<News>>(jsonwithNews.ToString());
            return json;
        }
    }

    
}
