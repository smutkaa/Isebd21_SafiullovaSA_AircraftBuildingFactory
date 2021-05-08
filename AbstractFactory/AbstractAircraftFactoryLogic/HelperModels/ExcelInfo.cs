using AbstractAircraftFactoryLogic.ViewModels;
using System.Collections.Generic;


namespace AbstractAircraftFactoryLogic.HelperModels
{
	class ExcelInfo
	{
		public string FileName { get; set; }
		public string Title { get; set; }
		public List<ReportAircraftComponentViewModel> AircraftComponents { get; set; }
		public List<ReportStorageComponentsViewModel> StorageComponents { get; set; }
	}
}
