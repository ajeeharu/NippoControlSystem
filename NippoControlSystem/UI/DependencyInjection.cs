using Microsoft.Extensions.DependencyInjection;
using NippoControlSystem.ApplicationService.Interfaces;
using NippoControlSystem.ApplicationService.Services;
using NippoControlSystem.UI.ViewModels;
using NippoControlSystem.UI.Views;

namespace NippoControlSystem.UI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // サービスの登録
            services.AddSingleton<INavigationService, NavigationService>();

            // View / ViewModel の登録
            services.AddTransient<OpeningViewModel>();
            services.AddTransient<OpeningView>();
            services.AddTransient<SettingView>();
            services.AddTransient<MainView>();
            services.AddTransient<TestHistoryView>();
            services.AddTransient<TopEditView>();
            services.AddTransient<VersionView>();

            return services;
        }
    }
}