using System.Windows;
using System.Windows.Controls;
using MoneyPlanner.ViewModel.Calculators;

namespace MoneyPlanner
{
    public partial class CompoundInterestCalculatorPage : UserControl
    {
        private CompoundInterestCalculatorViewModel _viewModel;
        public CompoundInterestCalculatorPage(CompoundInterestCalculatorViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            _viewModel = viewModel;
        }

        //Kliknutí spočítá výsledné hodnoty
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Calculate();
        }
    }
}
