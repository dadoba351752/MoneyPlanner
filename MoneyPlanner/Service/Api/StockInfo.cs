using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoneyPlanner.Service.Api
{
    public class StockInfo
    {
        [JsonPropertyName("bestMatches")]
        public List<SearchResult> BestMatches { get; set; }
    }
    public class SearchResult
    {
        [JsonPropertyName("1. symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("2. name")]
        public string Name { get; set; }
    }
    public class TodayPrice
    {
        [JsonPropertyName("Time Series (5min)")]
        public Dictionary<string, PriceResult> TimeSeries { get; set; }
    }
    public class PriceResult
    {
        [JsonPropertyName("1. open")]
        public string Open { get; set; }
        [JsonPropertyName("2. high")]
        public string High { get; set; }
        [JsonPropertyName("3. low")]
        public string Low { get; set; }
        [JsonPropertyName("4. close")]
        public string Close { get; set; }
        [JsonPropertyName("5. volume")]
        public string Volume { get; set; }
    }
}
