using System;
using System.Windows.Controls;

namespace MoneyPlanner.Service.Navigation
{
    public class NavigationService
    {
        public event Action<UserControl> NavigateRequested;

        public void NavigateTo(UserControl userControl)
        {
            NavigateRequested?.Invoke(userControl);
        }
    }
}
