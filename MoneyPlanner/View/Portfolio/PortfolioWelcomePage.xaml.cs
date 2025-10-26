using MoneyPlanner.Service.Database;
using MoneyPlanner.ViewModel.Portfolio;
using System.Windows;
using System.Windows.Controls;
using MoneyPlanner.Service.Navigation;
using MoneyPlanner.Service.DTO;

namespace MoneyPlanner.View.Portfolio
{
    public partial class PortfolioWelcomePage : UserControl
    {
        private NavigationService _navigationService;
        public PortfolioWelcomePage(NavigationService navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
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
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (PortfolioWelcomeViewModel)this.DataContext;
            string birthNumber = vm.BirthNumberSearchTextBox;

            if (birthNumber != null)
            {
                user = repository.FindUserByBirthNumber(birthNumber);
                vm.UserFoundButtonText = user.Name + " " + user.Surname;
                vm.UserFoundButtonIsEnabled = true;
                vm.ClearCreateSearchBoxes();
            }
        }

        private void UserFoundButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo(new PortfolioUserPage(_navigationService, user));
        }
    }
}
