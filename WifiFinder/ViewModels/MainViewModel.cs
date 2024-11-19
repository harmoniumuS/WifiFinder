using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WifiFinder.Data;
using WifiFinder.Models;
using WifiFinder.Services;

namespace WifiFinder.ViewModels
{
    public class MainViewModel: INotifyPropertyChanged
    {
        public string BestNetwork
        {
            get => _bestNetwork;
            set
            {
                if (_bestNetwork != value)
                {
                    _bestNetwork = value;
                    OnPropertyChanged(nameof(BestNetwork));
                }
            }
        }
        public ObservableCollection<WifiNetwork> Networks { get; }
        public ICommand ScanNetworksCommand { get; }
        public ICommand SaveNetworksCommand { get; }
        public event PropertyChangedEventHandler? PropertyChanged;
        
        private readonly DataBaseService _dataBaseService;
        private readonly WifiScannerService _wifiScannerService;
        private string _bestNetwork;
        private WifiNetwork _bestWifiNetwork;
        private ILogger<MainViewModel> _logger;
        
        public MainViewModel(ILogger<MainViewModel> logger, DataBaseService dataBaseService, WifiScannerService wifiScannerService)
        {
            _logger = logger;
            _dataBaseService = dataBaseService;
            _wifiScannerService = wifiScannerService;

            Networks = new ObservableCollection<WifiNetwork>();
            ScanNetworksCommand = new RelayCommand(ScanNetworks);
            SaveNetworksCommand = new RelayCommand(SaveNetworks);
        }
        //Метод производящий сканирование ближайших сетей и сохраняющий список сетей и лучшую сеть в список.
        public void ScanNetworks()
        {
            // Сканируем сети
            var scannedNetworks = _wifiScannerService.ScanNetworks();

            // Очищаем старые данные
            Networks.Clear();

            // Добавляем новые сети в ObservableCollection
            foreach (var network in scannedNetworks)
            {
                Networks.Add(network);
            }

            // Получаем сеть с наилучшим качеством сигнала
            var bestNetwork = _wifiScannerService.GetBestWifiNetwork(scannedNetworks);

            if (bestNetwork != null)
            {
                BestNetwork = $"{bestNetwork.SSID} ({bestNetwork.ConnectionQuality}%)";
                _bestWifiNetwork = bestNetwork; // сохраняем лучшую сеть для дальнейших операций
            }
            else
            {
                BestNetwork = "Нет доступных сетей";
                _bestWifiNetwork = null;
            }
        }
        //Метод сохраняет список сетей в базу данных
        public async void SaveNetworks()
        {
            if (Networks != null && Networks.Any())
            {
                try
                {
                    await _dataBaseService.SaveNetworksAsync(Networks.ToList());
                    MessageBox.Show("Все доступные сети сохранены в базу данных!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при сохранении данных: {ex.Message}");
                }

            }
            else
            {
                MessageBox.Show("Нет доступных сетей для сохранения.");
            }
        }

        // Метод для уведомления об изменении свойства
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
