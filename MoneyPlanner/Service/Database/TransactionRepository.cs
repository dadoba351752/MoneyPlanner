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
                    var inv = new InvestmentSumDTO();
                    inv.Name = reader.GetString(0);
                    inv.Symbol = reader.GetString(1);
                    inv.Amount = Convert.ToDecimal(reader.GetString(2));
                    inv.AverageBuyPrice = Convert.ToDecimal(reader.GetString(3));
                    inv.UserId = userId;
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
                    TransactionDTO tr = new TransactionDTO();
                    tr.Id = int.Parse(reader.GetString(0));
                    tr.Name = reader.GetString(1);
                    tr.Symbol = reader.GetString(3);
                    tr.Price = int.Parse(reader.GetString(4));
                    tr.Amount = int.Parse(reader.GetString(5));
                    tr.Volume = int.Parse(reader.GetString(6));
                    tr.Date = reader.GetString(7);
                    transactions.Add(tr);
                }
                return transactions;
            }
        }
    }
}
