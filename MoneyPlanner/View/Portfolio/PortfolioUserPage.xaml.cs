using System.Windows;
using System.Windows.Controls;
using MoneyPlanner.Service.Navigation;
using MoneyPlanner.ViewModel.Portfolio;
using MoneyPlanner.Service.DTO;
using MoneyPlanner.Service.Api;

namespace MoneyPlanner.View.Portfolio
{
    public partial class PortfolioUserPage : UserControl
    {
        private NavigationService _navigationService;
        private UserDTO _user;
        public PortfolioUserPage(NavigationService navigationService, UserDTO user)
        {
            InitializeComponent();
            _user = user;
            _navigationService = navigationService;
            DataContext = new PortfolioUserViewModel(user);
        }
        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo(new PortfolioWelcomePage(_navigationService));
        }

        private void AddInvestmentButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo(new PortfolioAddTransactionPage(_navigationService, _user));
        }

        private void ShowTransactionsButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo(new PortfolioUserTransactionsPage(_navigationService, _user));
        }

        private void ManageInvestmentsButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
