﻿using AbstractAircraftFactoryLogic.Interfaces;

namespace AbstractAircraftFactoryLogic.HelperModels
{
    public class MailCheckInfo
    { 
        public string PopHost { get; set; }
        public int PopPort { get; set; }
        public IMessageInfoLogic Logic { get; set; }
    }
}
