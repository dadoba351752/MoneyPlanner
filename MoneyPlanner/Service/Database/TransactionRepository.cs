using MoneyPlanner.Service.DTO;
using MoneyPlanner.Service.Interfaces;
using MoneyPlanner.View.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MoneyPlanner.Service.Database
{
    public class TransactionRepository : ITransactionRepository
    {
        private IMessageService _messageService;
        public TransactionRepository(IMessageService messageService)
        {
            _messageService = messageService;
        }

        //Přidá transakci
        public bool AddTransaction(TransactionDTO transaction)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText =
                    @"INSERT INTO Transactions (Name, Symbol, Price, Currency, Amount, Volume, Date, UserId)
                    VALUES (@Name, @Symbol, @Price, @Currency, @Amount, @Volume, @Date, @UserId)";

                command.Parameters.AddWithValue("@Name", transaction.Name);
                command.Parameters.AddWithValue("@Symbol", transaction.Symbol);
                command.Parameters.AddWithValue("@Price", transaction.Price);
                command.Parameters.AddWithValue("@Currency", transaction.Currency);
                command.Parameters.AddWithValue("@Amount", transaction.Amount);
                command.Parameters.AddWithValue("@Volume", transaction.Volume);
                command.Parameters.AddWithValue("@Date", transaction.Date);
                command.Parameters.AddWithValue("@UserId", transaction.UserId);
                try
                {
                    command.ExecuteNonQuery();
                    _messageService.ShowInformation("Investice byla úspěšná, budete přesměrováni.");
                    return true;
                }
                catch
                {
                    _messageService.ShowError("Je potřeba nejdříve potvrdit CP.");
                    return false;
                }
            }
        }
        //Vrátí kolekci investic daného uživatele sesumovanou podle názvu a symbolu
        public List<InvestmentSumDTO> GetInvestmentSum(int userId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText =
                    @"SELECT Name, Symbol, SUM(Amount) AS TotalAmount, SUM(Price * Amount)/SUM(Amount) AS AveragePrice, Currency
                    FROM Transactions
                    WHERE UserId = @UserId
                    GROUP BY Symbol, Name";

                command.Parameters.AddWithValue("@UserId", userId);
                var reader = command.ExecuteReader();
                List<InvestmentSumDTO> InvestmentSum = new List<InvestmentSumDTO>();

                while (reader.Read())
                {
                    var inv = new InvestmentSumDTO
                    {
                        Name = reader.GetString(0),
                        Symbol = reader.GetString(1),
                        Amount = Convert.ToDecimal(reader.GetString(2)),
                        AverageBuyPrice = Convert.ToDecimal(reader.GetString(3)),
                        Currency = reader.GetString(4),
                        UserId = userId
                    };
                    InvestmentSum.Add(inv);
                }
                return InvestmentSum;
            }
        }

        public ObservableCollection<TransactionDTO> GetTransactions(UserDTO user)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM Transactions WHERE UserId = @UserId";
                command.Parameters.AddWithValue("UserId", user.Id);
                var reader = command.ExecuteReader();
                ObservableCollection<TransactionDTO> transactions = new ObservableCollection<TransactionDTO>();

                while (reader.Read())
                {
                    TransactionDTO tr = new TransactionDTO
                    {
                        Id = int.Parse(reader.GetString(0)),
                        Name = reader.GetString(1),
                        Symbol = reader.GetString(2),
                        Price = int.Parse(reader.GetString(3)),
                        Currency = reader.GetString(4),
                        Amount = int.Parse(reader.GetString(5)),
                        Volume = int.Parse(reader.GetString(6)),
                        Date = reader.GetString(7),
                        UserId = int.Parse(reader.GetString(8))
                    };
                    transactions.Add(tr);
                }
                return transactions;
            }
        }
    }
}
