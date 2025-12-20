using MoneyPlanner.Service.Database;
using MoneyPlanner.Service.DTO;
using MoneyPlanner.Service.Enum;
using MoneyPlanner.Service.Interfaces;
using MoneyPlanner.View.Helpers;
using System;
using System.Collections.Generic;

namespace MoneyPlanner.Service.Settings
{
    public class CurrencySettings : ICurrencySettings
    {
        public CurrencySettings()
        {
        }

        //Vrátí list dostupných měn
        public List<CurrenciesEnum> GetCurrenciesList()
        {
            List<CurrenciesEnum> CurrenciesList = new List<CurrenciesEnum>
            {
                CurrenciesEnum.CZK,
                CurrenciesEnum.EUR,
                CurrenciesEnum.USD,
                CurrenciesEnum.GBP
            };
            return CurrenciesList;
        }

        //Vrátí nastavenou měnu
        public CurrenciesEnum GetCurrency()
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT Value FROM Settings WHERE Name = 'Currency'";

                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return CurrencyStringToEnum(reader.GetString(0));
                }
                else return CurrenciesEnum.CZK;
            }
        }

        //Nastaví měnu
        public void SetCurrency(CurrenciesEnum currency)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = connection.CreateCommand();
                string currencyText = CurrencyEnumToString(currency);
                command.CommandText = @"UPDATE Settings SET Value = @Currency WHERE Name = 'Currency';";
                command.Parameters.AddWithValue("@Currency", currencyText);
                command.ExecuteNonQuery();
            }
        }

        //Přemění currency enum na string
        public string CurrencyEnumToString(CurrenciesEnum currency)
        {
            switch (currency)
            {
                case CurrenciesEnum.CZK:
                    return "CZK";
                case CurrenciesEnum.EUR:
                    return "EUR";
                case CurrenciesEnum.USD:
                    return "USD";
                case CurrenciesEnum.GBP:
                    return "GBP";
                default:
                    return "CZK";
            }
        }

        //Přemění string na currency enum
        public CurrenciesEnum CurrencyStringToEnum(string currency)
        {
            switch (currency)
            {
                case "CZK":
                    return CurrenciesEnum.CZK;
                case "EUR":
                    return CurrenciesEnum.EUR;
                case "USD":
                    return CurrenciesEnum.USD;
                case "GBP":
                    return CurrenciesEnum.GBP;
                default:
                    return CurrenciesEnum.CZK;
            }
        }

        //Vrátí list zdrojů měnových kurzů
        public List<CurrencySourceEnum> GetCurrencySourceList()
        {
            List<CurrencySourceEnum> currencySourceList = new List<CurrencySourceEnum>
            {
                CurrencySourceEnum.Online,
                CurrencySourceEnum.Offline
            };
            return currencySourceList;
        }

        //Nastaví zdroj pro měnové kurzy
        public void SetCurrencySource(CurrencySourceEnum currencySource)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = connection.CreateCommand();
                string currencySourceString;
                if (currencySource == CurrencySourceEnum.Online)
                {
                    currencySourceString = "Online";
                } else currencySourceString = "Offline";
                command.CommandText = @"UPDATE Settings SET Value = @CurrencySource WHERE Name = 'Currency source';";
                command.Parameters.AddWithValue("@CurrencySource", currencySourceString);
                command.ExecuteNonQuery();
            }
        }

        //Vrátí aktuálně nastavený zdroj měnových kurzů
        public CurrencySourceEnum GetCurrencySource()
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT Value FROM Settings WHERE Name = 'Currency source'";

                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.GetString(0) == "Online")
                    {
                        return CurrencySourceEnum.Online;
                    }
                    else return CurrencySourceEnum.Offline;
                }
                return CurrencySourceEnum.Offline;
            }
        }

        public decimal? GetOfflineExchangeRate(string currencyFrom, string currencyTo)
        {
            var exchangeRates = new List<ExchangeRateDTO>
            {
                new ExchangeRateDTO { From = "CZK", To = "EUR", Rate = 0.04m },
                new ExchangeRateDTO { From = "CZK", To = "USD", Rate = 0.05m },
                new ExchangeRateDTO { From = "CZK", To = "GBP", Rate = 0.04m },

                new ExchangeRateDTO { From = "EUR", To = "CZK", Rate = 24.35m },
                new ExchangeRateDTO { From = "EUR", To = "USD", Rate = 1.17m },
                new ExchangeRateDTO { From = "EUR", To = "GBP", Rate = 0.88m },

                new ExchangeRateDTO { From = "USD", To = "CZK", Rate = 20.77m },
                new ExchangeRateDTO { From = "USD", To = "EUR", Rate = 0.85m },
                new ExchangeRateDTO { From = "USD", To = "GBP", Rate = 0.75m },

                new ExchangeRateDTO { From = "GBP", To = "CZK", Rate = 27.88m },
                new ExchangeRateDTO { From = "GBP", To = "EUR", Rate = 1.14m },
                new ExchangeRateDTO { From = "GBP", To = "USD", Rate = 1.34m }
            };
            foreach(var er in exchangeRates)
            {
                if(er.From == currencyFrom && er.To == currencyTo)
                {
                    return er.Rate;
                }
            }
            return null;
        }
    }
}
