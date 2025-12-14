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
        private MessageService messageService = new MessageService();
        private UserDTO user = new UserDTO();
        private UserRepository repository = new UserRepository();
        public PortfolioWelcomePage(NavigationService navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
            var vm = new PortfolioWelcomeViewModel();
            DataContext = vm;
            vm.UserFoundTextIsVisible = "Hidden";
            vm.UserFoundButtonIsVisible = "Hidden";
        }
        //Kliknutím vytvoří nového uživatele
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (PortfolioWelcomeViewModel)this.DataContext;
            string name = vm.NameCreateTextBox;
            string surname = vm.SurnameCreateTextBox;
            string birthNumber = vm.BirthNumberCreateTextBox;
            vm.ClearCreateSearchBoxes();

            //Validace na vyplněné hodnoty
            if (name != null && surname != null && birthNumber != null)
            {
                repository.AddUser(name, surname, birthNumber);
            }
            else messageService.ShowError("Nebyly zadány některé hodnoty.");
        }
        //Kliknutím vyhledá uživatele a zobrazí tlačítko s textblockem
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
                    vm.UserFoundButtonIsVisible = "Visible";
                    vm.UserFoundTextIsVisible = "Visible";
                    vm.ClearCreateSearchBoxes();
                }
                else messageService.ShowError("Uživatel nebyl nalezen, zkus to prosím znovu.");
            }
        }
        //Kliknutím přesměruje uživatele na uživatelskou stránku
        private void UserFoundButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo(new PortfolioUserPage(_navigationService, user));
        }
    }
}
