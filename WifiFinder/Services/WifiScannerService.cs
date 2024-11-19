using WifiFinder.Models;
using SimpleWifi;


namespace WifiFinder.Services
{
    public class WifiScannerService
    {
        private Wifi _wifi;
        public WifiScannerService()
        {
            _wifi = new Wifi();
        }

        // Метод для сканирования всех доступных сетей
        public List<WifiNetwork> ScanNetworks()
        {
            var networks = new List<WifiNetwork>();

            try
            {
                // Получаем доступные точки доступа
                var accessPoints = _wifi.GetAccessPoints();

                // Проверяем, если доступные сети не пусты
                if (accessPoints != null && accessPoints.Count > 0)
                {
                    foreach (var accessPoint in accessPoints)
                    {
                        // Добавляем информацию о сети в список
                        networks.Add(new WifiNetwork
                        {
                            SSID = accessPoint.Name, // Получаем SSID
                            ConnectionQuality = accessPoint.SignalStrength // Получаем силу сигнала
                        });
                    }

                    // Выводим информацию о найденных сетях
                    Console.WriteLine("Доступные сети:");
                    foreach (var network in networks)
                    {
                        Console.WriteLine($"SSID: {network.SSID}, Signal: {network.ConnectionQuality}%");
                    }
                }
                else
                {
                    Console.WriteLine("Не удалось найти доступные сети.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сканировании сетей: {ex.Message}");
            }

            return networks;
        }

        // Метод для получения сети с наилучшим качеством сигнала
        public WifiNetwork GetBestWifiNetwork(List<WifiNetwork> networks)
        {
            if (networks == null || networks.Count == 0)
                return null;  // Если нет сетей, возвращаем null

            // Сортируем сети по качеству сигнала (от наилучшего к худшему) и возвращаем первую
            return networks.OrderByDescending(n => n.ConnectionQuality).FirstOrDefault();
        }
    }

}
