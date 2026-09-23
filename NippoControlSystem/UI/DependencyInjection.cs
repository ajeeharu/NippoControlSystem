using Microsoft.Extensions.DependencyInjection;
using NippoControlSystem.ApplicationService.Interfaces;
using NippoControlSystem.ApplicationService.Services;
using NippoControlSystem.Domain.Interfaces;
using NippoControlSystem.Infrastructure.Devices;
using NippoControlSystem.Infrastructure.NativeLibs;
using NippoControlSystem.UI.ViewModels;
using NippoControlSystem.UI.Views;

namespace NippoControlSystem.UI
{
    public static class DependencyInjection
    {
        //　------　MVVMパターン用にリファクタリングしたコード　------

        //　------　MVVM化のためにリファクタリングする前のコード　------

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Device / Hardware 依存クラスの登録(シングルトンとして登録)
            services.AddSingleton<ICaioDevice, CaioDevice>();
            services.AddSingleton<Aio>();
            services.AddSingleton<ICdioDevice, CdioDevice>();

            // サービスの登録(シングルトンとして登録)
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