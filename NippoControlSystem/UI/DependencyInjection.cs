using Microsoft.Extensions.DependencyInjection;
using NippoControlSystem.ApplicationService.Interfaces;
using NippoControlSystem.ApplicationService.Services;
using NippoControlSystem.Domain.Interfaces;
using NippoControlSystem.Infrastructure.Devices;
using NippoControlSystem.Infrastructure.NativeLibs;
using NippoControlSystem.Infrastructure.Services;
using NippoControlSystem.UI.ViewModels;
using NippoControlSystem.UI.Views;

namespace NippoControlSystem.UI;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // ----------------------------------------------------
        // 1. Log / Infrastructure ラッパー
        // ----------------------------------------------------
        services.AddTransient(typeof(IAppLogger<>), typeof(AppLogger<>));

        // ----------------------------------------------------
        // 2. Device / Hardware 依存クラス
        // ----------------------------------------------------
        services.AddSingleton<ICaioDevice, CaioDevice>();
        services.AddSingleton<AnalogIO>();
        services.AddSingleton<ICdioDevice, CdioDevice>();

        // ----------------------------------------------------
        // 3. Application / Domain サービス
        // ----------------------------------------------------
        services.AddSingleton<INavigationService, NavigationService>();

        // ----------------------------------------------------
        // 4. View / ViewModel 登録
        // ----------------------------------------------------
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