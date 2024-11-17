using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WifiFinder.Models;

namespace WifiFinder.Data
{
    public class DataBaseService
    {
        private readonly AppDbContext _dbContext;

        public DataBaseService()
        { 
        _dbContext = new AppDbContext();
        }
        public async Task SaveNetworksAsync(List<WifiNetwork> networks)
        {
            try
            {
                _dbContext.WifiNetworks.AddRange(networks);
                await _dbContext.SaveChangesAsync();
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
            }
        }
    }
}
