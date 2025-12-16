using MoneyPlanner.Service.Api;
using MoneyPlanner.Service.Database;
using MoneyPlanner.Service.DTO;
using MoneyPlanner.Service.Navigation;
using MoneyPlanner.ViewModel.Portfolio;
using MoneyPlanner.Service.Interfaces;
using System;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;

namespace MoneyPlanner.View.Portfolio
{
    public partial class PortfolioAddTransactionPage : UserControl
    {
        private INavigationService _navigationService;
        private PortfolioAddTransactionViewModel _viewModel;
        private UserDTO _user;
        public PortfolioAddTransactionPage(INavigationService navigationService, IUserContext userContext, PortfolioAddTransactionViewModel viewModel)
        {
            InitializeComponent();
            _navigationService = navigationService;
            _user = userContext.CurrentUser;
            _viewModel = viewModel;
            DataContext = _viewModel;
        }
        //Potvrdí název akcie z textboxu a skrz API doplní její název a symbol
        private async void ConfirmInvestmentNameButton_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.ConfirmInvestmentName();
        }
        //Uloží transakci do databáze a přesměruje uživatele zpět na uživatelskou stránku
        private void ConfirmInvestmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.ConfirmInvestment())
            {
                _navigationService.NavigateTo<PortfolioUserPage>();
            }
        }
    }
}
