using AbstractAircraftFactoryLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractAircraftFactoryLogic.HelperModels
{
    class ExcelInfoStorage
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportStorageComponentsViewModel> StorageComponents { get; set; }
    }
}
