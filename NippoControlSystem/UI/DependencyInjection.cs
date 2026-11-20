using Microsoft.Extensions.DependencyInjection;
using NippoControlSystem.ApplicationService.Interfaces;
using NippoControlSystem.ApplicationService.Services;
using NippoControlSystem.ApplicationServices.Services;
using NippoControlSystem.Domain.Interfaces;
using NippoControlSystem.Infrastructure.Devices;
using NippoControlSystem.Infrastructure.NativeLibs;
using NippoControlSystem.Infrastructure.Persistence;
using NippoControlSystem.Infrastructure.Services;
using NippoControlSystem.UI.ViewModels;
using NippoControlSystem.UI.Views;
using System.Runtime.Versioning;

namespace NippoControlSystem.UI;

[SupportedOSPlatform("windows")]
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // ----------------------------------------------------
        // 1. アプリ状態管理（Singletonで一括保持）
        // ----------------------------------------------------
        //
        //　GeneralSettings,MenuInfoSettingsへ機能を移管

        // ----------------------------------------------------
        // 2. Log / Infrastructure ラッパー
        // ----------------------------------------------------
        services.AddTransient(typeof(IAppLogger<>), typeof(AppLogger<>));

        // ----------------------------------------------------
        // 3. Device / Hardware 依存クラス
        // ----------------------------------------------------
        services.AddSingleton<ICaioDevice, CaioDevice>();
        services.AddSingleton<AnalogIO>();
        services.AddSingleton<ICdioDevice, CdioDevice>();
        services.AddSingleton<Ai2Di>();
        services.AddSingleton<IOperationPanelService, OperationPanelService>();

        // ----------------------------------------------------
        // 3. FileI/O 依存クラス
        // ----------------------------------------------------
        services.AddSingleton<FileIoHelper>();
        services.AddSingleton<JsonFileRepository>();
        services.AddSingleton<CsvFileRepository>();


        // ----------------------------------------------------
        // 4. Application / Domain サービス
        // ----------------------------------------------------
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<InspectionDataService>();

        // ----------------------------------------------------
        // 5. View / ViewModel 登録
        // ----------------------------------------------------
        services.AddTransient<OpeningViewModel>();
        services.AddTransient<OpeningView>();
        services.AddTransient<InspectionViewModel>();
        services.AddTransient<InspectionMainView>();
        services.AddTransient<SettingView>();
        services.AddTransient<TestHistoryView>();
        services.AddTransient<TopEditView>();
        services.AddTransient<VersionView>();

        return services;
    }
}