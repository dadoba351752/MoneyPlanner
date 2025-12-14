using MoneyPlanner.Service.Api;
using MoneyPlanner.Service.Database;
using MoneyPlanner.Service.DTO;
using MoneyPlanner.View.Helpers;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MoneyPlanner.ViewModel.Portfolio
{
    public class PortfolioAddTransactionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        UserDTO _user = new UserDTO();
        TransactionRepository transactionRepository = new TransactionRepository();
        TransactionDTO transactionToConfirm = new TransactionDTO();
        MessageService _messageService = new MessageService();
        public PortfolioAddTransactionViewModel(UserDTO user)
        {
            _user = user;
            NameTextBlock = _user.Name + " " + _user.Surname + " ID: " + _user.Id;
            int CurrentYear = DateTime.Now.Year;
            int CurrentMonth = DateTime.Now.Month;
            int CurrentDay = DateTime.Now.Day;
            
            InvestmentDateTextBox = new DateTime(CurrentYear, CurrentMonth, CurrentDay);
        }

        public string _nameTextBlock;
        public string _investmentNameTextBox;
        public string _investmentAmountTextBox;
        public string _investmentPriceTextBox;
        private string _investmentVolumeTextBox;
        public DateTime _investmentDateTextBox;

        public async Task ConfirmInvestmentName()
        {
            var transaction = await AlphaVantageClient.SymbolSearch(InvestmentNameTextBox);
            if (transaction != null)
            {
                InvestmentNameTextBox = transaction.Name + " (" + transaction.Symbol + ")";
                transactionToConfirm.Name = transaction.Name;
                transactionToConfirm.Symbol = transaction.Symbol;
                transactionToConfirm.Currency = transaction.Currency;
            }
            else _messageService.ShowError("Něco se nepovedlo, zkus to prosím znovu.");
        }
        public void ConfirmInvestment()
        {
            transactionToConfirm.Price = int.Parse(InvestmentPriceTextBox);
            transactionToConfirm.Amount = int.Parse(InvestmentAmountTextBox);
            transactionToConfirm.Volume = transactionToConfirm.Price * transactionToConfirm.Amount;
            InvestmentVolumeTextBox = transactionToConfirm.Volume.ToString();
            transactionToConfirm.Date = InvestmentDateTextBox.ToString();
            transactionToConfirm.UserId = _user.Id;
            transactionRepository.AddTransaction(transactionToConfirm);
        }
        public string NameTextBlock
        {
            get { return _nameTextBlock; }
            set
            {
                _nameTextBlock = value;
                OnPropertyChanged(nameof(NameTextBlock));
            }
        }
        public string InvestmentNameTextBox 
        { 
            get { return _investmentNameTextBox; }
            set 
            {
                _investmentNameTextBox = value;
                OnPropertyChanged(nameof(InvestmentNameTextBox));
            } 
        }
        public string InvestmentAmountTextBox
        {
            get { return _investmentAmountTextBox; }
            set
            {
                _investmentAmountTextBox = value;
                OnPropertyChanged(nameof(InvestmentAmountTextBox));
            }
        }
        public string InvestmentPriceTextBox
        {
            get { return _investmentPriceTextBox; }
            set
            {
                _investmentPriceTextBox = value;
                OnPropertyChanged(nameof(InvestmentPriceTextBox));
            }
        }
        public string InvestmentVolumeTextBox
        {
            get { return _investmentVolumeTextBox; }
            set
            {
                _investmentVolumeTextBox = value;
                OnPropertyChanged(nameof(InvestmentVolumeTextBox));
            }
        }
        public DateTime InvestmentDateTextBox
        {
            get { return _investmentDateTextBox; }
            set
            {
                _investmentDateTextBox = value;
                OnPropertyChanged(nameof(InvestmentDateTextBox));
            }
        }
    }
}
