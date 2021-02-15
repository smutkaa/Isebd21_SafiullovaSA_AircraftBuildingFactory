using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractAircraftFactoryLogic.BindingModels
{
    public class CreateOrderBindingModel
    {
        /// <summary>
        /// Данные от клиента, для создания заказа
        /// </summary>
        public int AircraftId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
