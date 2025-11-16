using System.Windows;

namespace MoneyPlanner.View.Helpers
{
    public class MessageService : IMessageService
    {
        public void ShowError(string message)
        {
            MessageBox.Show(message, "Došlo k chybě.", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public bool ShowWarning(string message)
        {
            var result = MessageBox.Show(message, "Varování", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ShowInformation(string message)
        {
            MessageBox.Show(message, "Informace", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public bool ShowConfirmation(string message)
        {
            var result = MessageBox.Show(message, "Potvrzení", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if(result == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ShowQuestion(string message)
        {
            var result = MessageBox.Show(message, "Otázka", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
