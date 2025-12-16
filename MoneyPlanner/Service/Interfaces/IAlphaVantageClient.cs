using MoneyPlanner.Service.DTO;
using System.Threading.Tasks;

namespace MoneyPlanner.Service.Interfaces
{
    public interface IAlphaVantageClient
    {
        Task<TransactionDTO> SymbolSearch(string symbol);
        Task<InvestmentSumDTO> GetTodayPrice(InvestmentSumDTO investment);
        Task<decimal> GetCurrencyExchangeRate(string currencyFrom, string currencyTo);
    }
}
