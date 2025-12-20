using MoneyPlanner.Service.Api;
using MoneyPlanner.Service.Database;
using MoneyPlanner.Service.DTO;
using MoneyPlanner.Service.Enum;
using MoneyPlanner.Service.Interfaces;
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
        UserDTO _user;
        ITransactionRepository _transactionRepository;
        ICurrencySettings _currencySettings;
        private IAlphaVantageClient _apiClient;
        private decimal totalValue = 0;
        private decimal investedMoney;
        private string currency;
        private CurrencySourceEnum currencySource;
        public PortfolioUserViewModel(IUserContext userContext, IAlphaVantageClient apiClient, ITransactionRepository transactionRepository, ICurrencySettings currencySettings)
        {
            _user = userContext.CurrentUser;
            _apiClient = apiClient;
            _transactionRepository = transactionRepository;
            _currencySettings = currencySettings;

            //Pomocné proměnné
            currency = _currencySettings.CurrencyEnumToString(_currencySettings.GetCurrency());
            currencySource = _currencySettings.GetCurrencySource();

            //Název
            UserHeaderTextBlock = "Vítejte, " + _user.Name + " " + _user.Surname;
            //Box vlevo
            PortfolioValueTextBox = "Aktualizuj.";

            //Box vpravo
            investedMoney = decimal.Parse(_transactionRepository.GetInvestedMoney(_user.Id));
            InvestedMoneyTextBox = investedMoney + " " + currency;
            InvestmentsCountTextBox = "Celkově v " + (_transactionRepository.GetInvestmentSum(_user.Id).Count).ToString() + " instrumentech.";
        }
        public async Task SetTotalValue(UserDTO user)
        //public void SetTotalValue(UserDTO user)
        {
            var investmentSum = _transactionRepository.GetInvestmentSum(user.Id);
            for (var i = 0; i < investmentSum.Count; i++)
            {
                //var inv = AlphaVantageClient.GetTodayPrice(investmentSum[i]);
                //Pokud fejkuju, nepoužívám řádek níže a naopak
                var inv = await _apiClient.GetTodayPrice(investmentSum[i]);
                if(inv.Currency != currency)
                {
                    decimal rate;
                    rate = (currencySource == CurrencySourceEnum.Online)
                        ? await _apiClient.GetCurrencyExchangeRate(inv.Currency, currency)
                        : (decimal)_currencySettings.GetOfflineExchangeRate(inv.Currency, currency);

                    totalValue += rate * (inv.Amount * inv.TodayPrice);
                } else totalValue += (inv.Amount * inv.TodayPrice);
            }
            PortfolioValueTextBox = totalValue + " " + currency;
            SetInvestmentGrowth(totalValue, investedMoney);
        }
        public void SetInvestmentGrowth(decimal liveValue, decimal investedMoney)
        {
            decimal investmentGrowthMoney = liveValue - investedMoney;
            InvestmentGrowthAmount = investmentGrowthMoney.ToString();
            decimal investmentGrowthPercentage = ((liveValue - investedMoney) / (investedMoney)) * 100;
            InvestmentGrowthPercentage = (investmentGrowthMoney > 0) ? ("↗️ " + investmentGrowthPercentage + "%") : ("↘️" + investmentGrowthPercentage + "%");
            InvestmentGrowthColor = (investmentGrowthMoney > 0) ? "Green" : "Red";
        }
        private string _userHeaderTextBlock;
        private string _portfolioValueTextBox;
        private string _investmentsCountTextBox;
        private string _investedMoneyTextBox;
        private string _investmentGrowthAmount;
        private string _investmentGrowthPercentage;
        private string _investmentGrowthColor;
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

        public string InvestmentsCountTextBox 
        { 
            get { return _investmentsCountTextBox; }
            set
            {
                _investmentsCountTextBox = value;
                OnPropertyChanged(nameof(InvestmentsCountTextBox));
            }
        }

        public string InvestedMoneyTextBox
        {
            get { return _investedMoneyTextBox; }
            set
            {
                _investedMoneyTextBox = value;
                OnPropertyChanged(nameof(InvestedMoneyTextBox));
            }
        }
        public string InvestmentGrowthAmount
        {
            get { return _investmentGrowthAmount; }
            set
            {
                _investmentGrowthAmount = value;
                OnPropertyChanged(nameof(InvestmentGrowthAmount));
            }
        }
        public string InvestmentGrowthPercentage
        {
            get { return _investmentGrowthPercentage; }
            set
            {
                _investmentGrowthPercentage = value;
                OnPropertyChanged(nameof(InvestmentGrowthPercentage));
            }
        }
        public string InvestmentGrowthColor
        {
            get { return _investmentGrowthColor; }
            set
            {
                _investmentGrowthColor = value;
                OnPropertyChanged(nameof(InvestmentGrowthColor));
            }
        }
    }
}
