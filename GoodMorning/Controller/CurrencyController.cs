using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GoodMorning
{
    public enum CurrencyIndexEnum
    {
        EUR, USD, GBP
    }
    public class CurrencyController
    {
       
        public string[] currencySymbols { get; }
        private const string URL = "http://api.nbp.pl/api/exchangerates/rates/A/";
        private string urlParameters;
        private HttpClient client;
        public CurrencyController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            currencySymbols = new string[] { "EUR", "USD", "GBP" } ;

        }

        public string GetData(string currencySymbol)
        {
            HttpResponseMessage httResponse;
            urlParameters = currencySymbol.Insert(currencySymbol.Length, "/");
            try
            {
                httResponse = client.GetAsync(urlParameters).Result;
                if (httResponse.IsSuccessStatusCode)
                {
                    string response = httResponse.Content.ReadAsStringAsync().Result;
                    Console.Write(response);
                    return ParseJSONWithCurrency(response);
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)httResponse.StatusCode, httResponse.ReasonPhrase);
                    return null;
                }
            }
            catch (AggregateException e)
            {
                Console.WriteLine("Cannot get data from api");
                return null;
            }

        }

        private string ParseJSONWithCurrency(string response)
        {
            string[] responseSplitted = response.Split(',');
            StringBuilder value = new StringBuilder();
            //tha last value is value with currency
            value.Append(responseSplitted[responseSplitted.Length - 1]);//ading [ because it was in first part os responseSplitted          
            value.Remove(value.Length - 3, 3);//removing last characters }]}
            value.Remove(0, 6);//removing {mid: at the begining
            return value.ToString();

        }
    }
}
