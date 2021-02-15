﻿using AbstractAircraftFactoryLogic.Enums;
using System;
using System.ComponentModel;

namespace AbstractAircraftFactoryLogic.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int AircraftId { get; set; }
        [DisplayName("Изделие")]
        public string AircraftName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
        [DisplayName("Сумма")]
        public decimal Sum { get; set; }
        [DisplayName("Статус")]
        public OrderStatus Status { get; set; }
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}
