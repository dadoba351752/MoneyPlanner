using System.Windows;
using System.Windows.Controls;
using MoneyPlanner.ViewModel.Calculators;

namespace MoneyPlanner
{
    public partial class CompoundInterestCalculatorPage : UserControl
    {
        public CompoundInterestCalculatorPage()
        {
            InitializeComponent();
            DataContext = new CompoundInterestCalculatorViewModel();
        }

        //Kliknutí spočítá výsledné hodnoty
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (CompoundInterestCalculatorViewModel)this.DataContext;
            vm.Calculate();
        }
    }
}
