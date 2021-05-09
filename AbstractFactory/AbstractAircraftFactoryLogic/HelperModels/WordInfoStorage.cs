using System;
using System.Collections.Generic;
using System.Text;
using AbstractAircraftFactoryLogic.ViewModels;

namespace AbstractAircraftFactoryLogic.HelperModels
{
    class WordInfoStorage
    {
		public string FileName { get; set; }
		public string Title { get; set; }
		public List<StorageViewModel> Storages { get; set; }
	}
}