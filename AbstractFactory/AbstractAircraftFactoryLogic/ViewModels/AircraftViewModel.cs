using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractAircraftFactoryLogic.ViewModels
{
	[DataContract]
	public class AircraftViewModel
	{
		[DataMember]
		public int Id { get; set; }
		[DataMember]
		[DisplayName("Название самолета")]
		public string AircraftName { get; set; }
		[DataMember]
		[DisplayName("Цена")]
		public decimal Price { get; set; }
		[DataMember]
		public Dictionary<int, (string, int)> AircraftComponents { get; set; }
	}
}
