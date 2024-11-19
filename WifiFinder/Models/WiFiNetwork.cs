using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WifiFinder.Models
{
    public class WifiNetwork
    {
        [Key]
        public int Id { get; set; }
        public string SSID { get; set; }
        public uint ConnectionQuality { get; set; }
    }
}
