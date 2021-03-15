using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AbstractFactoryDatabaseImplement.Models
{
    public class AircraftComponent
    {
        public int Id { get; set; }
        public int AircraftId { get; set; }
        public int ComponentId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Component Component { get; set; }
        public virtual Aircraft Aircraft { get; set; }
    }
}
