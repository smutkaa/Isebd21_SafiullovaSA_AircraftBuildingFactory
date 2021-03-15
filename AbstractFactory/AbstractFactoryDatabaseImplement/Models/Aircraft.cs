using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AbstractFactoryDatabaseImplement.Models
{
    public class Aircraft
    {
        public int Id { get; set; }
        [Required]
        public string AircraftName { get; set; }
        [Required]
        public decimal Price { get; set; }

        public virtual AircraftComponent AircraftComponents { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
