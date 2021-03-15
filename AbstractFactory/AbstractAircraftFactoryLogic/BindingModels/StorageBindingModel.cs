using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractAircraftFactoryLogic.BindingModels
{
    public class StorageBindingModel
    {
        public int? Id { get; set; }
        public string StorageName { get; set; }
        public string ResponsiblePerson { get; set; }
        public DateTime DateCreate { get; set; }
        public Dictionary<int, (string,int)> StorageComponents { get; set; }
    }
}
