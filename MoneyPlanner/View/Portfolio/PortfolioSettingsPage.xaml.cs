using MoneyPlanner.Service.DTO;
using MoneyPlanner.Service.Navigation;
using MoneyPlanner.Service.Settings;
using MoneyPlanner.View.Helpers;
using MoneyPlanner.ViewModel.Portfolio;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MoneyPlanner.View.Portfolio
{
    public partial class PortfolioSettingsPage : UserControl
    {
        private NavigationService _navigationService;
        private UserDTO _user;
        private readonly MessageService messageService = new MessageService();
        CurrencySettings currencySettings = new CurrencySettings();
        public PortfolioSettingsPage(NavigationService navigationService, UserDTO user)
        {
            InitializeComponent();
            var vm = new PortfolioSettingsViewModel();
            DataContext = vm;
            _navigationService = navigationService;
            _user = user;
        }
        //Kliknutím uloží nastavení
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (PortfolioSettingsViewModel)this.DataContext;
            try
            {
                currencySettings.SetCurrency(vm.SelectedCurrency);
                messageService.ShowInformation("Uloženo.");
            } catch(Exception ex)
            {
                messageService.ShowError(ex.Message);
            }
        }
        //Kliknutím přesměruje uživatele zpět na uživatelskou stránku
        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo(new PortfolioUserPage(_navigationService, _user));
        }
    }
}
