using MoneyPlanner.Service.Interfaces;
using MoneyPlanner.View.Helpers;
using MoneyPlanner.ViewModel.Portfolio;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MoneyPlanner.View.Portfolio
{
    public partial class PortfolioSettingsPage : UserControl
    {
        private INavigationService _navigationService;
        private readonly IMessageService _messageService;
        private PortfolioSettingsViewModel _viewModel;
        private ICurrencySettings _currencySettings;
        public PortfolioSettingsPage(ICurrencySettings currencySettings, INavigationService navigationService, IMessageService messageService, PortfolioSettingsViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            _currencySettings = currencySettings;
            _navigationService = navigationService;
            _messageService = messageService;
        }
        //Kliknutím uloží nastavení
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _currencySettings.SetCurrency(_viewModel.SelectedCurrency);
                _currencySettings.SetCurrencySource(_viewModel.SelectedCurrencySource);
                _messageService.ShowInformation("Uloženo.");
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }
        //Kliknutím přesměruje uživatele zpět na uživatelskou stránku
        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo<PortfolioUserPage>();
        }
    }
}
