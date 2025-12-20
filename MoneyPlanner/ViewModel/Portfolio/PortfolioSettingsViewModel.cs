using MoneyPlanner.Service.Enum;
using MoneyPlanner.Service.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace MoneyPlanner.ViewModel.Portfolio
{
    public class PortfolioSettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public PortfolioSettingsViewModel(ICurrencySettings currencySettings)
        {
            CurrencyComboBox = currencySettings.GetCurrenciesList();
            _selectedCurrency = currencySettings.GetCurrency();
            CurrencySourceComboBox = currencySettings.GetCurrencySourceList();
            _selectedCurrencySource = currencySettings.GetCurrencySource();
        }

        public List<CurrenciesEnum> _currencyComboBox;
        public CurrenciesEnum _selectedCurrency;
        public List<CurrencySourceEnum> _currencySourceComboBox;
        public CurrencySourceEnum _selectedCurrencySource;
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
        public List<CurrencySourceEnum> CurrencySourceComboBox
        {
            get { return _currencySourceComboBox; }
            set
            {
                _currencySourceComboBox = value;
                OnPropertyChanged(nameof(CurrencySourceComboBox));
            }
        }
        public CurrencySourceEnum SelectedCurrencySource
        {
            get { return _selectedCurrencySource; }
            set
            {
                _selectedCurrencySource = value;
                OnPropertyChanged(nameof(SelectedCurrencySource));
            }
        }
    }
}
