using MoneyPlanner.Service.Database;
using MoneyPlanner.Service.DTO;
using MoneyPlanner.View.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MoneyPlanner.ViewModel.Portfolio
{
    public class PortfolioUserTransactionsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<TransactionDTO> _transactionsDataGrid;
        public ObservableCollection<TransactionDTO> TransactionsDataGrid
        {
            get { return _transactionsDataGrid; }
            set
            {
                _transactionsDataGrid = value;
                OnPropertyChanged(nameof(TransactionsDataGrid));
            }
        }
        public void SetDataGrid(ObservableCollection<TransactionDTO> transactions)
        {
            TransactionsDataGrid = transactions;
        }
    }
}
