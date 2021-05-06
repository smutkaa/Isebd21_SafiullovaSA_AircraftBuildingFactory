using AbstractAircraftFactoryLogic.Attributes;
using AbstractAircraftFactoryLogic.Enums;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractAircraftFactoryLogic.ViewModels
{
    [DataContract]
    public class OrderViewModel
    {
        [Column(title: "Номер", width: 50)]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public int AircraftId { get; set; }
        [DataMember]
        public int? ImplementerId { get; set; }

        [Column(title: "Изделие", gridViewAutoSize: GridViewAutoSize.AllCells)]
        [DataMember]
        [DisplayName("Изделие")]
        public string AircraftName { get; set; }

        [Column(title: "Клиент", width: 150)]
        [DataMember]
        [DisplayName("Клиент")]
        public string ClientName { get; set; }

        [Column(title: "Исполнитель", width: 150)]
        [DataMember]
        [DisplayName("Исполнитель")]
        public string ImplementerName { get; set; }
        [Column(title: "Количество", gridViewAutoSize: GridViewAutoSize.AllCells)]
        [DataMember]
        [DisplayName("Количество")]
        public int Count { get; set; }
        [Column(title: "Сумма", width: 50)]
        [DataMember]
        [DisplayName("Сумма")]
        public decimal Sum { get; set; }
        [Column(title: "Статус", width: 100)]
        [DataMember]
        [DisplayName("Статус")]
        public OrderStatus Status { get; set; }
        [Column(title: "Дата создания", width: 100)]
        [DataMember]
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
        [Column(title: "Дата выполнения", width: 100)]
        [DataMember]
        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}
