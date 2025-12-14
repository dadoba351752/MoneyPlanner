using MoneyPlanner.Service.Database;
using MoneyPlanner.Service.DTO;
using MoneyPlanner.Service.Navigation;
using MoneyPlanner.ViewModel.Portfolio;
using System.Windows;
using System.Windows.Controls;

namespace MoneyPlanner.View.Portfolio
{
    public partial class PortfolioUserTransactionsPage : UserControl
    {
        private NavigationService _navigationService;
        private UserDTO _user;
        private static readonly TransactionRepository transactionRepository = new TransactionRepository();
        public PortfolioUserTransactionsPage(NavigationService navigationService, UserDTO user)
        {
            InitializeComponent();
            _navigationService = navigationService;
            _user = user;
            var vm = new PortfolioUserTransactionsViewModel();
            DataContext = vm;

            //Načte transakce s ID uživatele a pošle je do datagridu
            var transactions = transactionRepository.GetTransactions(_user);
            vm.TransactionsDataGrid = transactions;
        }
        //Přesměruje uživatele zpět na uživatelskou stránku
        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo(new PortfolioUserPage(_navigationService, _user));
        }
    }
}
