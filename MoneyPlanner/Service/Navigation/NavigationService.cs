using Microsoft.Extensions.DependencyInjection;
using MoneyPlanner.Service.Interfaces;
using System;
using System.Windows.Controls;

namespace MoneyPlanner.Service.Navigation
{
    public class NavigationService : INavigationService
    {
        private IServiceProvider _provider;
        public NavigationService(IServiceProvider provider)
        {
            _provider = provider;
        }

        public event Action<UserControl> NavigateRequested;
        public void NavigateTo<TPage>() where TPage : UserControl
        {
            var page = _provider.GetRequiredService<TPage>();
            NavigateRequested?.Invoke(page);
        }
    }
}
