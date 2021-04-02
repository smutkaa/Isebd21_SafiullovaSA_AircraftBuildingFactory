using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace AbstractAircraftFactoryLogic.BindingModels
{
    [DataContract]
    public class ClientBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}
