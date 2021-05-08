using System;
using System.Collections.Generic;

namespace AbstractAircraftFactoryLogic.ViewModels
{
	public class ReportAircraftComponentViewModel
	{
		public string AircraftName { get; set; }
		public int TotalCount { get; set; }
		public List<Tuple<string, int>> Components { get; set; }
	}
}
