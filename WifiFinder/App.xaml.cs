using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Data;
using System.Windows;
using WifiFinder.Data;
using WifiFinder.Services;
using WifiFinder.ViewModels;

namespace WifiFinder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
       
            private readonly IServiceProvider _serviceProvider;

            public App()
            {
                var serviceCollection = new ServiceCollection();

                // Настройка логгера
                serviceCollection.AddLogging(builder =>
                {
                    builder.AddConsole();  // Логирование в консоль
                    builder.AddDebug();    // Логирование в отладчик
                });
                                
                // Регистрация DbContext с правильной строкой подключения
                serviceCollection.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlite("Data Source=wifi.db"); // Исправлена ошибка в строке подключения
                });

                // Регистрация сервисов
                serviceCollection.AddScoped<DataBaseService>();
                serviceCollection.AddSingleton<WifiScannerService>();

                // Регистрация ViewModel и MainWindow
                serviceCollection.AddTransient<MainViewModel>();
                serviceCollection.AddSingleton<MainWindow>();

                // Создание контейнера DI
                _serviceProvider = serviceCollection.BuildServiceProvider();
            }

            protected override void OnStartup(StartupEventArgs e)
            {
                base.OnStartup(e);

                // Получаем экземпляр MainWindow и MainViewModel из DI контейнера
                var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
                var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();

                // Привязываем ViewModel к MainWindow
                mainWindow.DataContext = mainViewModel;
               
            
                 // Показываем окно
                mainWindow.Show();
            
                
            }
        }

    }
