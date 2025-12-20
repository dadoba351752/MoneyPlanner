using Microsoft.Extensions.DependencyInjection;
using MoneyPlanner.Service.Api;
using MoneyPlanner.Service.Calculators;
using MoneyPlanner.Service.Context;
using MoneyPlanner.Service.Database;
using MoneyPlanner.Service.Interfaces;
using MoneyPlanner.Service.Navigation;
using MoneyPlanner.Service.Settings;
using MoneyPlanner.View.Helpers;
using MoneyPlanner.View.Home;
using MoneyPlanner.View.Portfolio;
using MoneyPlanner.ViewModel.Calculators;
using MoneyPlanner.ViewModel.Portfolio;
using System.Windows;

namespace MoneyPlanner
{
    /// <summary>
    /// Interakční logika pro App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();
            ConfigureServices(services);

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<PortfolioSettingsPage>();

            var mainWindow = provider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            //Page
            services.AddTransient<MainWindow>();
            services.AddTransient<HomePage>();
            services.AddTransient<CompoundInterestCalculatorPage>();
            services.AddTransient<NetIncomeCalculatorPage>();
            services.AddTransient<PortfolioAddTransactionPage>();
            services.AddTransient<PortfolioSettingsPage>();
            services.AddTransient<PortfolioUserPage>();
            services.AddTransient<PortfolioUserTransactionsPage>();
            services.AddTransient<PortfolioWelcomePage>();

            //ViewModel
            services.AddTransient<CompoundInterestCalculatorViewModel>();
            services.AddTransient<NetIncomeCalculatorViewModel>();
            services.AddTransient<PortfolioAddTransactionViewModel>();
            services.AddTransient<PortfolioSettingsViewModel>();
            services.AddTransient<PortfolioUserTransactionsViewModel>();
            services.AddTransient<PortfolioUserViewModel>();
            services.AddTransient<PortfolioWelcomeViewModel>();

            //Service
            services.AddSingleton<CompoundInterestCalculatorService>();
            services.AddSingleton<NetIncomeCalculatorService>();
            services.AddSingleton<IMessageService, MessageService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IAlphaVantageClient, AlphaVantageClient>();
            services.AddSingleton<ITransactionRepository, TransactionRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ICurrencySettings, CurrencySettings>();
            services.AddSingleton<IUserContext, UserContext>();
        }
    }
}
