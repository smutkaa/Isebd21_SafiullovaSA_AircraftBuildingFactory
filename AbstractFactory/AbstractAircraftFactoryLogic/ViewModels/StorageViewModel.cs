using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AbstractAircraftFactoryLogic.ViewModels
{
    public class StorageViewModel
    {
        public int Id { get; set; }
        [DisplayName("Склад")]
        public string StorageName { get; set; }

        [DisplayName("Ответственное лицо")]
        public string ResponsiblePerson { get; set; }
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        public Dictionary<int, (string, int)> StorageComponent { get; set; }
    }
}
