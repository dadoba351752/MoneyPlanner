using System.Windows;
using System.Windows.Controls;
using MoneyPlanner.Service.Navigation;
using MoneyPlanner.ViewModel.Portfolio;
using MoneyPlanner.Service.DTO;
using MoneyPlanner.Service.Api;
using System.Threading.Tasks;

namespace MoneyPlanner.View.Portfolio
{
    public partial class PortfolioUserPage : UserControl
    {
        private NavigationService _navigationService;
        private UserDTO _user;
        private PortfolioUserViewModel vm;
        public PortfolioUserPage(NavigationService navigationService, UserDTO user)
        {
            InitializeComponent();
            _user = user;
            _navigationService = navigationService;
            DataContext = new PortfolioUserViewModel(user);
            vm = (PortfolioUserViewModel)DataContext;
        }

        //Přesměruje uživatele zpět na úvodní stránku
        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo(new PortfolioWelcomePage(_navigationService));
        }
        //přesměruje uživatele na stránku pro přidání transakce
        private void AddInvestmentButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo(new PortfolioAddTransactionPage(_navigationService, _user));
        }
        //Přesměruje uživatele na stránku se seznamem transakcí
        private void ShowTransactionsButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo(new PortfolioUserTransactionsPage(_navigationService, _user));
        }
        //Přesměruje uživatele na stránku s nastavení
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo(new PortfolioSettingsPage(_navigationService, _user));
        }
        private async void RefreshPortfolioValueButton_Click(object sender, RoutedEventArgs e)
        {
            await vm.SetTotalValue(_user);
        }
    }
}
