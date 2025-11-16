using MoneyPlanner.Service.DTO;
using MoneyPlanner.View.Helpers;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace MoneyPlanner.Service.Api
{
    public class AlphaVantageClient
    {
        public static MessageService _messageService = new MessageService();
        private const string ApiKey = "MYZUAUUP9MOXKG61";
        
        private static readonly HttpClient client = new HttpClient();
        //https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords=tsla&apikey=MYZUAUUP9MOXKG61

        public static async Task<TransactionDTO> SymbolSearch(string symbol)
        {
            var url = $"https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords={symbol}&apikey={ApiKey}";

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<StockInfo>(json);
            TransactionDTO transaction = new TransactionDTO();

            try
            {
                var first = result.BestMatches.First();
                transaction.Name = first.Name;
                transaction.Symbol = first.Symbol;
                return transaction;
            } catch(Exception ex)
            {
                _messageService.ShowError(ex.Message);
                return null;
            }
        }

        //Zakomentované řádky jsou z důvodu denních limitů API na 25 volání denně.
        //Je nahraný soubor TSLA.json který obsahuje symbol: TSLA, testovat na BirthNumber = 000
        //Async metoda se používá v případě využití reálného API
        public static InvestmentSumDTO GetTodayPrice(InvestmentSumDTO investment)
        //public static async Task<InvestmentSumDTO> GetTodayPrice(InvestmentSumDTO investment)
        {
            //Řádky níže se využívají v případě použití reálného API
            //var url = $"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={investment.Symbol}&interval=5min&apikey={ApiKey}";
            //https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=TSLA&interval=5min&apikey=MYZUAUUP9MOXKG61
            //var response = await client.GetAsync(url);
            //response.EnsureSuccessStatusCode();
            //string json = await response.Content.ReadAsStringAsync();

            //Stringy níže jsou pro fake souborové API TSLA
            string BaseDir = AppContext.BaseDirectory;
            string ProjectDir = Directory.GetParent(BaseDir).Parent.Parent.Parent.FullName;
            string fakeResponse = Path.Combine(ProjectDir, "Files", "TSLA.json");
            string json = File.ReadAllText(fakeResponse);

            var result = JsonSerializer.Deserialize<TodayPrice>(json);
            try
            {
                var first = result.TimeSeries.OrderByDescending(x => x.Key).First();
                var valueOpen = first.Value.Open;
                investment.TodayPrice = Math.Round(decimal.Parse(valueOpen, NumberStyles.Any, CultureInfo.InvariantCulture), 2);
            } catch(Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
            return investment;
        }
    }
}
