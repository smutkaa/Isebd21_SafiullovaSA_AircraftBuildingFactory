﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractAircraftFactoryLogic.BindingModels
{
    public class ImplementerBindingModel
    {
        public int? Id { get; set; }
        public string ImplementerName { get; set; }
        public int WorkingTime { get; set; }
        public int PauseTime { get; set; }
    }
}
