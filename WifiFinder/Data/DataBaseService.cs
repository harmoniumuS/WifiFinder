using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WifiFinder.Models;
using Microsoft.Extensions.Logging;

namespace WifiFinder.Data
{
    public class DataBaseService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger _logger;

        public DataBaseService(ILogger<DataBaseService> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public async Task SaveNetworksAsync(List<WifiNetwork> networks)
        {
            if (networks == null || networks.Count==0)
            {
                _logger.LogWarning("Список сетей пуст. Сохранение не выполнено.");
                return;
            }
            try
            {
                _dbContext.WifiNetworks.AddRange(networks);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Сети Wifi успешно добавлены.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении в базу данных: {ex.Message}");
            }
        }
        public async Task SaveNetworkAsync(WifiNetwork network)
        {
            try
            {
                _dbContext.WifiNetworks.Add(network);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении в базу данных: {ex.Message}");
                throw;
            }
        }
    }
}
