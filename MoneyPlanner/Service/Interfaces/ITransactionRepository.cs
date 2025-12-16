using MoneyPlanner.Service.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyPlanner.Service.Interfaces
{
    public interface ITransactionRepository
    {
        bool AddTransaction(TransactionDTO transaction);
        List<InvestmentSumDTO> GetInvestmentSum(int userId);
        ObservableCollection<TransactionDTO> GetTransactions(UserDTO user);
    }
}
