using System.Collections.Generic;
using System.ComponentModel;

namespace AbstractAircraftFactoryLogic.ViewModels
{
	public class AircraftViewModel
	{
		public int Id { get; set; }
		[DisplayName("Название самолета")]
		public string AircraftName { get; set; }
		[DisplayName("Цена")]
		public decimal Price { get; set; }
		public Dictionary<int, (string, int)> AircraftComponents { get; set; }
	}
}
