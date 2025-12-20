using MoneyPlanner.Service.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyPlanner.Service.Interfaces
{
    public interface ICurrencySettings
    {
        List<CurrenciesEnum> GetCurrenciesList();
        CurrenciesEnum GetCurrency();
        void SetCurrency(CurrenciesEnum currency);
        string CurrencyEnumToString(CurrenciesEnum currency);
        CurrenciesEnum CurrencyStringToEnum(string currency);
        List<CurrencySourceEnum> GetCurrencySourceList();
        void SetCurrencySource(CurrencySourceEnum currencySource);
        CurrencySourceEnum GetCurrencySource();
        decimal? GetOfflineExchangeRate(string currencyFrom, string currencyTo);
    }
}
