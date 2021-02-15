using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryListImplement.Models
{
    class Aircraft
    {
        public int Id { get; set; }
        public string AircraftName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, int> AircraftComponents { get; set; }
    }
}
