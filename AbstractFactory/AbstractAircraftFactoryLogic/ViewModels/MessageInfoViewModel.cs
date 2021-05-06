using AbstractAircraftFactoryLogic.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractAircraftFactoryLogic.ViewModels
{
    /// <summary>
    /// Сообщения, приходящие на почту
    /// </summary>
    [DataContract]
    public class MessageInfoViewModel
    {
        [DataMember]
        public string MessageId { get; set; }
        [Column(title: "Отправитель", gridViewAutoSize: GridViewAutoSize.AllCells)]
        [DisplayName("Отправитель")]
        [DataMember]
        public string SenderName { get; set; }

        [Column(title: "Номер", width: 150)]
        [DisplayName("Дата письма")]
        [DataMember]
        public DateTime DateDelivery { get; set; }

        [Column(title: "Заголовок", gridViewAutoSize: GridViewAutoSize.AllCells)]
        [DisplayName("Заголовок")]
        [DataMember]
        public string Subject { get; set; }

        [Column(title: "Текст", gridViewAutoSize: GridViewAutoSize.AllCells)]
        [DisplayName("Текст")]
        [DataMember]
        public string Body { get; set; }
    }
}
