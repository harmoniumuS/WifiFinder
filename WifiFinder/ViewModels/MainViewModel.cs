using CommunityToolkit.Mvvm.ComponentModel;
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
        private readonly DataBaseService _dataBaseService;
        private readonly WifiScannerService _wifiScannerService;
        private string _bestNetwork;
        private WifiNetwork _bestWifiNetwork;
        public event PropertyChangedEventHandler? PropertyChanged;

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

        public MainViewModel()
        {
            _dataBaseService = new DataBaseService();
            _wifiScannerService = new WifiScannerService();
            Networks = new ObservableCollection<WifiNetwork>();
            ScanNetworksCommand = new RelayCommand(ScanNetworks);
            SaveNetworksCommand = new RelayCommand(SaveNetworks);
        }

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

        // Метод для сохранения информации о сети (пока пустой, можете добавить логику для сохранения в базу данных или другой источник)
        public void SaveNetworks()
        {
            if (Networks != null)
            {
                _dataBaseService.SaveNetworksAsync(Networks.ToList());
                MessageBox.Show("Все доступные сети сохранены в базу данных!");
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
