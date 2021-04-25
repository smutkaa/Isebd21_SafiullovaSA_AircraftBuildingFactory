using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractFactoryDatabaseImplement.Models
{
    public class Component
    {
        public int Id { get; set; }
        [Required]
        // поле обязательно к заполнению
        public string ComponentName { get; set; }
        [ForeignKey("ComponentId")]
        public virtual List<AircraftComponent> AircraftComponents { get; set; }
    }
}