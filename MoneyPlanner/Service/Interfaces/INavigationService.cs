using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MoneyPlanner.Service.Interfaces
{
    public interface INavigationService
    {
        event Action<UserControl> NavigateRequested;
        void NavigateTo<TPage>() where TPage : UserControl;
    }
}
