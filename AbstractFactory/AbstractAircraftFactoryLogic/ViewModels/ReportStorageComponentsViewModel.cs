using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractAircraftFactoryLogic.ViewModels
{
    public class ReportStorageComponentsViewModel
    {
        public string Name { get; set; }

        public int Count { get; set; }

        public List<Tuple<string, int>> Components { get; set; }
    }
}
