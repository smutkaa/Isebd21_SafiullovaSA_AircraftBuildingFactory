using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractFactoryDatabaseImplement.Models
{
    public class Aircraft
    {
        public int Id { get; set; }

        [ForeignKey("AircraftId")]
        public virtual List<AircraftComponent> AircraftComponent { get; set; }

        [ForeignKey("AircraftId")]
        public virtual List<Order> Order { get; set; }

        [Required]
        public string AircraftName { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
