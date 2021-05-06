using AbstractAircraftFactoryLogic.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AbstractAircraftFactoryLogic.ViewModels
{
    public class ImplementerViewModel
    {
        public int Id { get; set; }
        [Column(title: "ФИО исполнителя", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DisplayName("ФИО исполнителя")]
        public string ImplementerName { get; set; }
        [Column(title: "Время на заказ", width: 100)]
        [DisplayName("Время на заказ")]
        public int WorkingTime { get; set; }
        [Column(title: "Время на перерыв", width: 100)]
        [DisplayName("Время на перерыв")]
        public int PauseTime { get; set; }

    }
}
