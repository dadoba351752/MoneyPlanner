using System.Windows.Controls;
using MoneyPlanner.ViewModel.Calculators;

namespace MoneyPlanner
{
    public partial class NetIncomeCalculatorPage : UserControl
    {
        public NetIncomeCalculatorPage()
        {
            InitializeComponent();
            DataContext = new NetIncomeCalculatorViewModel();
        }
    }
}
