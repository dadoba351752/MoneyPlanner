using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MoneyPlanner
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        NetIncomeCalculatorPage netIncomeCalculatorPage = new NetIncomeCalculatorPage ();
        CompoundInterestCalculatorPage compoundInterestCalculatorPage = new CompoundInterestCalculatorPage();
        private void NetIncomeCalculatorMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = netIncomeCalculatorPage;
        }

        private void CompoundInterestCalculatorMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = compoundInterestCalculatorPage;
        }
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
