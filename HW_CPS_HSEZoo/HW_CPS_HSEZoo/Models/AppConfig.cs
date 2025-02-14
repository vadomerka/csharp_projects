using System;
using Microsoft.Extensions.DependencyInjection;


namespace HW_CPS_HSEZoo.Models;

/// <summary>
/// Класс для единоразового вызова сервисов.
/// </summary>
public class AppConfig
{
    private static IServiceProvider? services;
    private static object lockObj = new object();

    public static IServiceProvider Services
    {
        get
        {
            lock (lockObj)
            {
                services ??= GetService();
                return services;
            }

        }

    }

    /// <summary>
    /// Добавление сервисов.
    /// </summary>
    /// <returns></returns>
    public static IServiceProvider GetService()
    {
        var services = new ServiceCollection();
        services.AddSingleton<HseZoo>();
        services.AddSingleton<VetClinic>();
        services.AddSingleton<InventoryFactory>();

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider;

    }

}
