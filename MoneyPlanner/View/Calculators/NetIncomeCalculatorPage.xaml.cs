using System.Windows.Controls;
using MoneyPlanner.ViewModel.Calculators;

namespace MoneyPlanner
{
    public partial class NetIncomeCalculatorPage : UserControl
    {
        public NetIncomeCalculatorPage(NetIncomeCalculatorViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
