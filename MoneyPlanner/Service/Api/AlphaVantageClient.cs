using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using static System.Net.WebRequestMethods;
using System.Windows;

namespace MoneyPlanner.Service.Api
{
    public class AlphaVantageClient
    {
        private const string ApiKey = "MYZUAUUP9MOXKG61";
        private static readonly HttpClient client = new HttpClient();
        //https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords=tsla&apikey=MYZUAUUP9MOXKG61

        public static async Task SymbolSearch(string symbol)
        {
            var url = $"https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords={symbol}&apikey={ApiKey}";

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<StockInfo>(json);
            var first = result.BestMatches.First();
            var name = first.Name;

            MessageBox.Show($"Název je: {name}");
        }


    }
}
