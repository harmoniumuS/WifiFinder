using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WifiFinder.Models
{
    public class WifiNetwork
    {
        public int Id { get; set; }
        public string SSID { get; set; }
        public uint ConnectionQuality { get; set; }
    }
}
