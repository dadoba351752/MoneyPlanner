using MoneyPlanner.Service.Database;
using System.Windows;
using MoneyPlanner.View.Portfolio;
using MoneyPlanner.Service.Navigation;
using System.Windows.Controls;
using MoneyPlanner.View.Helpers;
using MoneyPlanner.View.Home;

namespace MoneyPlanner
{
    public partial class MainWindow : Window
    {
        private NavigationService _navigationService = new NavigationService();
        public MainWindow()
        {
            InitializeComponent();
            DatabaseHelper.InitializeDatabase();
            _navigationService.NavigateRequested += OnNavigateRequested;
            MainContent.Content = new HomePage();
        }
        void OnNavigateRequested(UserControl userControl)
        {
            MainContent.Content = userControl;
        }

        private void NetIncomeCalculatorMenuButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo(new NetIncomeCalculatorPage());
        }
        private void CompoundInterestCalculatorMenuButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo(new CompoundInterestCalculatorPage());
        }
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            //Prozatím to nikam nevede, je nastaveno NavigateTo(null), protože nemám vytvořenou "startovací" UserControl
            _navigationService.NavigateTo(new HomePage());
        }
        private void PortfolioMenuButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateTo(new PortfolioWelcomePage(_navigationService));
        }
    }
}
