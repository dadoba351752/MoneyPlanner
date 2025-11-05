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
        [JsonPropertyName("2. name")]
        public string Name { get; set; }
    }
}
