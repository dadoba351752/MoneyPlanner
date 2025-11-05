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
        public PortfolioUserPage(NavigationService navigationService, UserDTO user)
        {
            InitializeComponent();
            _navigationService = navigationService;
            DataContext = new PortfolioUserViewModel(user);
        }
        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo(new PortfolioWelcomePage(_navigationService));
        }

        private void AddInvestmentButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowTransactionsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void ManageInvestmentsButton_Click(object sender, RoutedEventArgs e)
        {
            await AlphaVantageClient.SymbolSearch("TSLA");
        }
    }
}
