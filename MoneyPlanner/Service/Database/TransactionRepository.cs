using MoneyPlanner.Service.DTO;
using MoneyPlanner.View.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace MoneyPlanner.Service.Database
{
    public class TransactionRepository
    {
        public MessageService messageService = new MessageService();
        public void AddTransaction(TransactionDTO transaction)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText =
                    @"INSERT INTO Transactions (Name, Symbol, Price, Amount, Volume, Date, UserId)
                    VALUES (@Name, @Symbol, @Price, @Amount, @Volume, @Date, @UserId)";

                command.Parameters.AddWithValue("@Name", transaction.Name);
                command.Parameters.AddWithValue("@Symbol", transaction.Symbol);
                command.Parameters.AddWithValue("@Price", transaction.Price);
                command.Parameters.AddWithValue("@Amount", transaction.Amount);
                command.Parameters.AddWithValue("@Volume", transaction.Volume);
                command.Parameters.AddWithValue("@Date", transaction.Date);
                command.Parameters.AddWithValue("@UserId", transaction.UserId);
                try
                {
                    command.ExecuteNonQuery();
                    messageService.ShowInformation("Investice byla úspěšný, budete přesměrováni.");                }
                catch (Exception ex)
                {
                    messageService.ShowError(ex.Message);
                    //MessageBox.Show("Tato akce nelze provést.", "Došlo k chybě.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public List<InvestmentSumDTO> GetInvestmentSum(int userId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText =
                    @"SELECT Name, Symbol, SUM(Amount) AS TotalAmount, SUM(Price * Amount)/SUM(Amount) AS AveragePrice
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
                        Amount = int.Parse(reader.GetString(4)),
                        Volume = int.Parse(reader.GetString(5)),
                        Date = reader.GetString(6),
                        UserId = int.Parse(reader.GetString(7))
                    };
                    transactions.Add(tr);
                }
                return transactions;
            }
        }
    }
}
