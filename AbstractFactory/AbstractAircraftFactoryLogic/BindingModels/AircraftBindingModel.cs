using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractAircraftFactoryLogic.BindingModels
{
    public class AircraftBindingModel
    {
        public int? Id { get; set; }
        public string AircraftName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> AircraftComponents { get; set; }
    }
}
