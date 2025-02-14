using System;
using Microsoft.Extensions.DependencyInjection;


namespace HW_CPS_HSEZoo.Models;

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

    public static IServiceProvider GetService()
    {
        // Console.WriteLine("Get Services........");
        var services = new ServiceCollection();
        services.AddSingleton<HseZoo>();
        services.AddSingleton<VetClinic>();
        services.AddSingleton<InventoryFactory>();
        // services.AddSingleton<PedalCarFactory>(); // регистрируем сервис для создания педальных автомобилей
        // services.AddSingleton<HandCarFactory>(); // регистрируем сервис для создания автомобилей с ручным приводом

        // services.AddSingleton<ICarProvider>(sp => sp.GetRequiredService<CarService>()); // регистрируем интерфейс ICarProvider в качестве сервиса
        // services.AddSingleton<ICustomersProvider>(sp => sp.GetRequiredService<CustomersStorage>()); // регистрируем интерфейс ICustomersProvider в качестве сервиса

        // services.AddSingleton<HseCarService>(); // регистрируем сервис по продаже автомобилей

        var serviceProvider = services.BuildServiceProvider(); // строим контейнер зависимостей
        return serviceProvider;

    }

}
