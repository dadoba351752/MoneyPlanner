using System.Windows;
using System.Windows.Controls;
using MoneyPlanner.Service.Interfaces;
using MoneyPlanner.ViewModel.Portfolio;
using MoneyPlanner.Service.DTO;
using MoneyPlanner.Service.Api;
using System.Threading.Tasks;

namespace MoneyPlanner.View.Portfolio
{
    public partial class PortfolioUserPage : UserControl
    {
        private INavigationService _navigationService;
        private UserDTO _user = new UserDTO();
        private PortfolioUserViewModel _viewModel;
        public PortfolioUserPage(INavigationService navigationService, IUserContext userContext, PortfolioUserViewModel viewModel)
        {
            InitializeComponent();
            _user = userContext.CurrentUser;
            _navigationService = navigationService;
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        //Přesměruje uživatele zpět na úvodní stránku
        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo<PortfolioWelcomePage>();
        }
        //přesměruje uživatele na stránku pro přidání transakce
        private void AddInvestmentButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo<PortfolioAddTransactionPage>();
        }
        //Přesměruje uživatele na stránku se seznamem transakcí
        private void ShowTransactionsButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo<PortfolioUserTransactionsPage>();
        }
        //Přesměruje uživatele na stránku s nastavení
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo<PortfolioSettingsPage>();
        }
        private async void RefreshPortfolioValueButton_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.SetTotalValue(_user);
        }
    }
}
