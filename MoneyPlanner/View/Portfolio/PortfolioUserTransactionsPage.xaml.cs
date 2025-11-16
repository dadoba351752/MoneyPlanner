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
        private TransactionRepository transactionRepository = new TransactionRepository();
        public PortfolioUserTransactionsPage(NavigationService navigationService, UserDTO user)
        {
            InitializeComponent();
            _navigationService = navigationService;
            _user = user;
            DataContext = new PortfolioUserTransactionsViewModel();
            var vm = (PortfolioUserTransactionsPage)this.DataContext;
            var transactions = transactionRepository.GetTransactions(_user);
            
        }
        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo(new PortfolioUserPage(_navigationService, _user));
        }
    }
}
