using AbstractAircraftFactoryLogic.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractAircraftFactoryLogic.ViewModels
{
	[DataContract]
	public class AircraftViewModel
	{
		[Column(title: "Номер", width: 50)]
		[DataMember]
		public int Id { get; set; }

		[Column(title: "Название самолета", gridViewAutoSize: GridViewAutoSize.Fill)]
		[DataMember]
		[DisplayName("Название самолета")]
		public string AircraftName { get; set; }
		[Column(title: "Цена", width: 150)]
		[DataMember]
		[DisplayName("Цена")]
		public decimal Price { get; set; }
		[DataMember]
		public Dictionary<int, (string, int)> AircraftComponents { get; set; }
	}
}
