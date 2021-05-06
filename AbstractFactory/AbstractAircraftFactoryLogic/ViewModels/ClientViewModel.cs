using AbstractAircraftFactoryLogic.Attributes;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractAircraftFactoryLogic.ViewModels
{
    [DataContract]
    public class ClientViewModel
    {
        [Column(title: "Номер", width: 50, visible:false)]
        [DataMember]
        public int? Id { get; set; }
        [Column(title: "ФИО", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        [DisplayName("ФИО")]
        public string ClientName { get; set; }
        [Column(title: "Логин", width: 150)]
        [DataMember]
        [DisplayName("Логин")]
        public string Login { get; set; }
        [Column(title: "Пароль", gridViewAutoSize: GridViewAutoSize.AllCells)]
        [DataMember]
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}
