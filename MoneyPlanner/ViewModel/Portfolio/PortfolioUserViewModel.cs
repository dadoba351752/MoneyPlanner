using MoneyPlanner.Service.Api;
using MoneyPlanner.Service.Database;
using MoneyPlanner.Service.DTO;
using MoneyPlanner.Service.Settings;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MoneyPlanner.ViewModel.Portfolio
{
    public class PortfolioUserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        UserDTO _user = new UserDTO();
        TransactionRepository transactionRepository = new TransactionRepository();
        CurrencySettings currencySettings = new CurrencySettings();
        private decimal totalValue = 0;
        private string currency;
        public PortfolioUserViewModel(UserDTO user)
        {
            _user = user;
            UserHeaderTextBlock = _user.Name + " " + _user.Surname;
            currency = currencySettings.CurrencyEnumToString(currencySettings.GetCurrency());
            PortfolioValueTextBox = "Pro získání aktuální hodnoty portfolia, klikněte zde: ";
        }
        public async Task SetTotalValue(UserDTO user)
        //public void SetTotalValue(UserDTO user)
        {
            var investmentSum = transactionRepository.GetInvestmentSum(user.Id);
            for (var i = 0; i < investmentSum.Count; i++)
            {
                //var inv = AlphaVantageClient.GetTodayPrice(investmentSum[i]);
                //Pokud fejkuju, nepoužívám řádek níže a naopak
                var inv = await AlphaVantageClient.GetTodayPrice(investmentSum[i]);
                if(inv.Currency != currency)
                {
                    var rate = await AlphaVantageClient.GetCurrencyExchangeRate(inv.Currency, currency);
                    totalValue += rate * (inv.Amount * inv.TodayPrice);
                } else
                {
                    totalValue += (inv.Amount * inv.TodayPrice);
                }
            }
            PortfolioValueTextBox = "Celková hodnota portfolia: " + totalValue + " " + currency;
        }
        private string _userHeaderTextBlock;
        private string _portfolioValueTextBox;
        public string UserHeaderTextBlock 
        { get { return _userHeaderTextBlock; }
            set
            {
                _userHeaderTextBlock = value;
                OnPropertyChanged(nameof(UserHeaderTextBlock));
            } 
        }
        public string PortfolioValueTextBox
        {
            get { return _portfolioValueTextBox; }
            set
            {
                _portfolioValueTextBox = value;
                OnPropertyChanged(nameof(PortfolioValueTextBox));
            }
        }
    }
}
