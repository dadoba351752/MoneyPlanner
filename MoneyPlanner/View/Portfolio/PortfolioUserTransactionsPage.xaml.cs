using MoneyPlanner.Service.Database;
using MoneyPlanner.Service.DTO;
using MoneyPlanner.Service.Interfaces;
using MoneyPlanner.Service.Navigation;
using MoneyPlanner.ViewModel.Portfolio;
using System.Windows;
using System.Windows.Controls;

namespace MoneyPlanner.View.Portfolio
{
    public partial class PortfolioUserTransactionsPage : UserControl
    {
        private INavigationService _navigationService;
        private ITransactionRepository _transactionRepository;
        private UserDTO _user;
        public PortfolioUserTransactionsPage(INavigationService navigationService, IUserContext userContext, ITransactionRepository transactionRepository, PortfolioUserTransactionsViewModel viewModel)
        {
            InitializeComponent();
            _transactionRepository = transactionRepository;
            _navigationService = navigationService;
            _user = userContext.CurrentUser;
            DataContext = viewModel;

            //Načte transakce s ID uživatele a pošle je do datagridu
            var transactions = _transactionRepository.GetTransactions(_user);
            viewModel.TransactionsDataGrid = transactions;
        }
        //Přesměruje uživatele zpět na uživatelskou stránku
        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo<PortfolioUserPage>();
        }
    }
}
