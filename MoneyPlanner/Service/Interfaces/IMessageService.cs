namespace MoneyPlanner.View.Helpers
{
    public interface IMessageService
    {
        void ShowError(string message);
        bool ShowWarning(string message);
        void ShowInformation(string message);
        bool ShowConfirmation(string message);
        bool ShowQuestion(string message);
    }
}
