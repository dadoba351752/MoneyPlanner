using MoneyPlanner.Service.Enum;
using MoneyPlanner.Service.Settings;
using System.Collections.Generic;
using System.ComponentModel;

namespace MoneyPlanner.ViewModel.Portfolio
{
    public class PortfolioSettingsViewModel : INotifyPropertyChanged
    {
        CurrencySettings currencySettings = new CurrencySettings();
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PortfolioSettingsViewModel()
        {
            CurrencyComboBox = currencySettings.GetCurrenciesList();
            _selectedCurrency = currencySettings.GetCurrency();
        }

        public List<CurrenciesEnum> _currencyComboBox = new List<CurrenciesEnum>();
        public CurrenciesEnum _selectedCurrency;
        public List<CurrenciesEnum> CurrencyComboBox
        {
            get { return _currencyComboBox; }
            set
            {
                _currencyComboBox = value;
                OnPropertyChanged(nameof(CurrencyComboBox));
            }
        }
        public CurrenciesEnum SelectedCurrency
        {
            get { return _selectedCurrency; }
            set
            {
                _selectedCurrency = value;
                OnPropertyChanged(nameof(SelectedCurrency));
            }
        }
    }
}
