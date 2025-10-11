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
    }
}
