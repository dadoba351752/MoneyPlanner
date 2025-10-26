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
        public PortfolioUserViewModel(UserDTO user)
        {
            _user = user;
            UserHeaderTextBlock = _user.Name + " " + _user.Surname;
        }

        private string _userHeaderTextBlock;
        public string UserHeaderTextBlock 
        { get { return _userHeaderTextBlock; }
            set
            {
                _userHeaderTextBlock = value;
                OnPropertyChanged(nameof(UserHeaderTextBlock));
            } 
        }
    }
}
