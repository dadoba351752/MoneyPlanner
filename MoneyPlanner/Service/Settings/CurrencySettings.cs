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
    }
}
