using MoneyPlanner.Service.Api;
using MoneyPlanner.Service.Database;
using MoneyPlanner.Service.DTO;
using System.ComponentModel;

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
        private decimal totalValue;
        public PortfolioUserViewModel(UserDTO user)
        {
            _user = user;
            UserHeaderTextBlock = _user.Name + " " + _user.Surname;
            SetTotalValue(user);
            PortfolioValueTextBox = $"Celková hodnota portfolia: " + totalValue + " fiktivních peněz :)";
        }
        private void SetTotalValue(UserDTO user)
        //private async void SetTotalValue(UserDTO user)
        {
            var investmentSum = transactionRepository.GetInvestmentSum(user.Id);
            foreach (var i in investmentSum)
            {
                var inv = AlphaVantageClient.GetTodayPrice(i);
                //Pokud fejkuju, nepoužívám řádek níže a naopak
                //var inv = await AlphaVantageClient.GetTodayPrice(i);
                totalValue += (inv.Amount * inv.TodayPrice);
            }
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
