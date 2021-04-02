using System;
using System.Collections.Generic;
using System.Text;
using AbstractAircraftFactoryLogic.Enums;

namespace AbstractAircraftFactoryLogic.ViewModels
{
	public class ReportOrdersViewModel
	{
		public DateTime DateCreate { get; set; }
		public string AircraftName { get; set; }
		public int Count { get; set; }
		public decimal Sum { get; set; }
		public string Status { get; set; }
	}
}
