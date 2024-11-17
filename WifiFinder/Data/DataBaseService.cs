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
            _dbContext.WifiNetworks.AddRange(networks);
            await _dbContext.SaveChangesAsync();
        }
        public async Task SaveNetworkAsync(WifiNetwork network)
        { 
            _dbContext.WifiNetworks.Add(network);
            await _dbContext.SaveChangesAsync();
        }
    }
}
