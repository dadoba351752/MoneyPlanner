using MoneyPlanner.View.Helpers;
using System.ComponentModel;

namespace MoneyPlanner.ViewModel.Portfolio
{
    public class PortfolioWelcomeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string _nameCreateTextBox;
        public string _surnameCreateTextBox;
        public string _birthNumberCreateTextBox;
        public string _birthNumberSearchTextBox;
        public string _userFoundButtonIsVisible;
        public string _userFoundTextIsVisible;
        public string _userFoundButtonText;

        public void ClearCreateSearchBoxes()
        {
            NameCreateTextBox = null;
            SurnameCreateTextBox = null;
            BirthNumberCreateTextBox = null;
            BirthNumberSearchTextBox = null;
        }
        public string NameCreateTextBox
        {
            get { return _nameCreateTextBox; }
            set
            {
                _nameCreateTextBox = value;
                OnPropertyChanged(nameof(NameCreateTextBox));
            }
        }
        public string SurnameCreateTextBox
        {
            get { return _surnameCreateTextBox; }
            set
            {
                _surnameCreateTextBox = value;
                OnPropertyChanged(nameof(SurnameCreateTextBox));
            }
        }
        public string BirthNumberCreateTextBox
        {
            get { return _birthNumberCreateTextBox; }
            set
            {
                _birthNumberCreateTextBox = value;
                OnPropertyChanged(nameof(BirthNumberCreateTextBox));
            }
        }
        public string BirthNumberSearchTextBox
        {
            get { return _birthNumberSearchTextBox; }
            set
            {
                _birthNumberSearchTextBox = value;
                OnPropertyChanged(nameof(BirthNumberSearchTextBox));
            }
        }

        public string UserFoundButtonIsVisible
        {
            get { return _userFoundButtonIsVisible; }
            set
            {
                _userFoundButtonIsVisible = value;
                OnPropertyChanged(nameof(UserFoundButtonIsVisible));
            }
        }

        public string UserFoundTextIsVisible
        {
            get { return _userFoundTextIsVisible; }
            set
            {
                _userFoundTextIsVisible = value;
                OnPropertyChanged(nameof(UserFoundTextIsVisible));
            }
        }

        public string UserFoundButtonText
        {
            get { return _userFoundButtonText; }
            set
            {
                _userFoundButtonText = value;
                OnPropertyChanged(nameof(UserFoundButtonText));
            }
        }
    }
}
