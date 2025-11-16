using MoneyPlanner.Service.Database;
using MoneyPlanner.ViewModel.Portfolio;
using System.Windows;
using System.Windows.Controls;
using MoneyPlanner.Service.Navigation;
using MoneyPlanner.Service.DTO;
using MoneyPlanner.View.Helpers;

namespace MoneyPlanner.View.Portfolio
{
    public partial class PortfolioWelcomePage : UserControl
    {
        private NavigationService _navigationService;
        private MessageService _messageService;
        public PortfolioWelcomePage(NavigationService navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
            _messageService = new MessageService();
            DataContext = new PortfolioWelcomeViewModel();
        }

        UserDTO user = new UserDTO();
        UserRepository repository = new UserRepository();
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (PortfolioWelcomeViewModel)this.DataContext;
            string name = vm.NameCreateTextBox;
            string surname = vm.SurnameCreateTextBox;
            string birthNumber = vm.BirthNumberCreateTextBox;
            vm.ClearCreateSearchBoxes();


            if (name != null && surname != null && birthNumber != null)
            {
                repository.AddUser(name, surname, birthNumber);
            }
            else _messageService.ShowError("Nebyly zadány některé hodnoty.");
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (PortfolioWelcomeViewModel)this.DataContext;
            string birthNumber = vm.BirthNumberSearchTextBox;

            if (birthNumber != null)
            {
                user = repository.FindUserByBirthNumber(birthNumber);
                if (user != null)
                {
                    vm.UserFoundButtonText = user.Name + " " + user.Surname;
                    vm.UserFoundButtonIsEnabled = true;
                    vm.ClearCreateSearchBoxes();
                }
                else _messageService.ShowError("Uživatel nebyl nalezen, zkus to prosím znovu.");
            }
        }

        private void UserFoundButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo(new PortfolioUserPage(_navigationService, user));
        }
    }
}
