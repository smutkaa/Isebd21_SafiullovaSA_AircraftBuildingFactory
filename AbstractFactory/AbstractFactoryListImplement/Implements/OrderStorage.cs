﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractAircraftFactoryLogic.Interfaces;
using AbstractAircraftFactoryLogic.ViewModels;
using AbstractAircraftFactoryLogic.BindingModels;
using AbstractFactoryListImplement.Models;

namespace AbstractFactoryListImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        private readonly DataListSingleton source;

        public OrderStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<OrderViewModel> GetFullList()
        {
            List<OrderViewModel> result = new List<OrderViewModel>();
            foreach (var order in source.Orders)
            {
                result.Add(CreateModel(order));
            }
            return result;
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<OrderViewModel> result = new List<OrderViewModel>();
            if (model.DateTo != null && model.DateFrom != null)
            {
                foreach (var order in source.Orders)
                {
                    if (order.DateCreate >= model.DateTo && order.DateCreate <= model.DateFrom)
                    {
                        result.Add(CreateModel(order));
                    }
                }
                return result;
            }
            foreach (var order in source.Orders)
            {
                if (order.AircraftId == model.AircraftId)
                {
                    result.Add(CreateModel(order));
                }
            }
            return result;
        }

        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var order in source.Orders)
            {
                if (order.Id == model.Id || order.AircraftId == model.AircraftId)
                {
                    return CreateModel(order);
                }
            }
            return null;
        }

        public void Insert(OrderBindingModel model)
        {
            Order tempOrder = new Order
            {
                Id = 1
            };
            foreach (var order in source.Orders)
            {
                if (order.Id >= tempOrder.Id)
                {
                    tempOrder.Id = order.Id + 1;
                }
            }
            source.Orders.Add(CreateModel(model, tempOrder));
        }

        public void Update(OrderBindingModel model)
        {
            Order tempOrder = null;
            foreach (var order in source.Orders)
            {
                if (order.Id == model.Id)
                {
                    tempOrder = order;
                }
            }
            if (tempOrder == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempOrder);
        }

        public void Delete(OrderBindingModel model)
        {
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id)
                {
                    source.Orders.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.AircraftId = model.AircraftId;
            order.ClientId = (int)model.ClientId;
            order.ImplementerId = model.ImplementerId;
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }

        private OrderViewModel CreateModel(Order order)
        {
            string aircraftName = null;
            foreach (var aircraft in source.Aircraft)
            {
                if (aircraft.Id == order.AircraftId)
                {
                    aircraftName = aircraft.AircraftName;
                }
            }

            string clientName = null;
            foreach (var client in source.Clients)
            {
                if (client.Id == order.ClientId)
                {
                    clientName = client.ClientName;
                }
            }
            string implementerName = null;
            foreach (var client in source.Implementers)
            {
                if (client.Id == order.ImplementerId)
                {
                    clientName = client.ImplementerName;
                }
            }
            return new OrderViewModel
            {
                Id = order.Id,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                AircraftId = order.AircraftId,
                AircraftName = aircraftName,
                ClientName = clientName,
                ImplementerId = order.ImplementerId,
                ImplementerName = implementerName
            };
        }
    }
} 

