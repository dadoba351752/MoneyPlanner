using MoneyPlanner.Service.DTO;
using MoneyPlanner.Service.Interfaces;
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
    public class AlphaVantageClient : IAlphaVantageClient
    {
        public static MessageService _messageService = new MessageService();
        private const string ApiKey = "MYZUAUUP9MOXKG61";
        
        private static readonly HttpClient client = new HttpClient();
        //https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords=tsla&apikey=MYZUAUUP9MOXKG61

        public async Task<TransactionDTO> SymbolSearch(string symbol)
        {
            //Delay cca 1 vteřina z důvodu free API omezení na 1 request za vteřinu
            await Task.Delay(1100);
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
                transaction.Currency = first.Currency;
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
        //public static InvestmentSumDTO GetTodayPrice(InvestmentSumDTO investment)
        public async Task<InvestmentSumDTO> GetTodayPrice(InvestmentSumDTO investment)
        {
            //Delay cca 1 vteřina z důvodu free API omezení na 1 request za vteřinu
            await Task.Delay(1100);

            //Řádky níže se využívají v případě použití reálného API
            var url = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={investment.Symbol}&interval=5min&apikey={ApiKey}";
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();

            //Řádky níže jsou pro fake souborové API TSLA
            //string BaseDir = AppContext.BaseDirectory;
            //string ProjectDir = Directory.GetParent(BaseDir).Parent.Parent.Parent.FullName;
            //string fakeResponse = Path.Combine(ProjectDir, "Files", "TSLA.json");
            //string json = File.ReadAllText(fakeResponse);

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

        //Vrací kurz pro currencyFrom/currencyTo
        public async Task<decimal> GetCurrencyExchangeRate(string currencyFrom, string currencyTo)
        {
            //Delay cca 1 vteřina z důvodu free API omezení na 1 request za vteřinu
            await Task.Delay(1100);
            decimal rate = 0;
            var url = $"https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency={currencyFrom}&to_currency={currencyTo}&apikey={ApiKey}";
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();

            var exchangeRate = JsonSerializer.Deserialize<CurrencyExchangeRate>(json);
            try
            {
                //Delay cca 1 vteřina z důvodu free API omezení na 1 request za vteřinu
                await Task.Delay(1100);
                rate = Math.Round(Decimal.Parse((exchangeRate.RealtimeCurrencyExchangeRate.ExchangeRate), NumberStyles.Any, CultureInfo.InvariantCulture), 2);
                return rate;

            } catch(Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
            return rate;
        }
    }
}
