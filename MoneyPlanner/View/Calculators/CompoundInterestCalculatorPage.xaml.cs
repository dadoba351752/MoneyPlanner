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

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if(DataContext is CompoundInterestCalculatorViewModel vm)
            {
                vm.Calculate();
            }
        }
    }
}
