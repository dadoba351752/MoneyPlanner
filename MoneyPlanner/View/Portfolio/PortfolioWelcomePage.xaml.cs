using MoneyPlanner.Service.Database;
using MoneyPlanner.ViewModel.Portfolio;
using System.Windows;
using System.Windows.Controls;
using MoneyPlanner.Service.Navigation;
using MoneyPlanner.Service.DTO;
using MoneyPlanner.View.Helpers;
using MoneyPlanner.Service.Interfaces;

namespace MoneyPlanner.View.Portfolio
{
    public partial class PortfolioWelcomePage : UserControl
    {
        private INavigationService _navigationService;
        private UserDTO user;
        private IUserContext _userContext;
        private IMessageService _messageService;
        private IUserRepository _userRepository;
        private PortfolioWelcomeViewModel _viewModel;
        public PortfolioWelcomePage(IUserContext userContext,INavigationService navigationService, IMessageService messageService, IUserRepository userRepository, PortfolioWelcomeViewModel viewModel)
        {
            InitializeComponent();
            user = userContext.CurrentUser;
            _userContext = userContext;
            _viewModel = viewModel;
            _navigationService = navigationService;
            _messageService = messageService;
            _userRepository = userRepository;
            DataContext = _viewModel;
            _viewModel.UserFoundTextIsVisible = "Hidden";
            _viewModel.UserFoundButtonIsVisible = "Hidden";
        }
        //Kliknutím vytvoří nového uživatele
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string name = _viewModel.NameCreateTextBox;
            string surname = _viewModel.SurnameCreateTextBox;
            string birthNumber = _viewModel.BirthNumberCreateTextBox;
            _viewModel.ClearCreateSearchBoxes();

            //Validace na vyplněné hodnoty
            if (name != null && surname != null && birthNumber != null)
            {
                _userRepository.AddUser(name, surname, birthNumber);
            }
            else _messageService.ShowError("Nebyly zadány některé hodnoty.");
        }
        //Kliknutím vyhledá uživatele a zobrazí tlačítko s textblockem
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string birthNumber = _viewModel.BirthNumberSearchTextBox;

            if (birthNumber != null)
            {
                user = _userRepository.FindUserByBirthNumber(birthNumber);
                if (user != null)
                {
                    _viewModel.UserFoundButtonText = user.Name + " " + user.Surname;
                    _viewModel.UserFoundButtonIsVisible = "Visible";
                    _viewModel.UserFoundTextIsVisible = "Visible";
                    _viewModel.ClearCreateSearchBoxes();
                }
                else _messageService.ShowError("Uživatel nebyl nalezen, zkus to prosím znovu.");
            }
        }
        //Kliknutím přesměruje uživatele na uživatelskou stránku
        private void UserFoundButton_Click(object sender, RoutedEventArgs e)
        {
            _userContext.CurrentUser = user;
            _navigationService.NavigateTo<PortfolioUserPage>();
        }
    }
}
