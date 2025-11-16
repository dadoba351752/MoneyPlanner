using MoneyPlanner.Service.Api;
using MoneyPlanner.Service.Database;
using MoneyPlanner.Service.DTO;
using MoneyPlanner.Service.Navigation;
using MoneyPlanner.ViewModel.Portfolio;
using System;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;

namespace MoneyPlanner.View.Portfolio
{
    public partial class PortfolioAddTransactionPage : UserControl
    {
        private NavigationService _navigationService;
        private UserDTO _user;
        public PortfolioAddTransactionPage(NavigationService navigationService, UserDTO user)
        {
            InitializeComponent();
            _navigationService = navigationService;
            _user = user;
            DataContext = new PortfolioAddTransactionViewModel(_user);
        }
        private void ConfirmInvestmentNameButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (PortfolioAddTransactionViewModel)this.DataContext;
            vm.ConfirmInvestmentName();
        }
        private void ConfirmInvestmentButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (PortfolioAddTransactionViewModel)this.DataContext;
            vm.ConfirmInvestment();
            _navigationService.NavigateTo(new PortfolioUserPage(_navigationService, _user));
        }
    }
}
