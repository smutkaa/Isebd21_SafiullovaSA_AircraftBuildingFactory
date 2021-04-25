using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.Interfaces;
using AbstractAircraftFactoryLogic.ViewModels;
using AbstractFactoryDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AbstractAircraftFactoryLogic.Enums;

namespace AbstractFactoryDatabaseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using (var context = new AbstractFactoryDatabase())
            {
                return context.Orders
                    .Include(rec => rec.Aircraft)
                    .Include(rec => rec.Client)
                    .Include(rec => rec.Implementer)
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    AircraftName = rec.Aircraft.AircraftName,
                    AircraftId = rec.AircraftId,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    Status = rec.Status,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                    ClientId = rec.ClientId,
                    ClientName = rec.Client.ClientName,
                    ImplementerId = rec.ImplementerId,
                    ImplementerName = rec.ImplementerId.HasValue ? rec.Implementer.ImplementerName : string.Empty
                })
                .ToList();
            }
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new AbstractFactoryDatabase())
            {
                return context.Orders
                    .Include(rec => rec.Aircraft)
                    .Include(rec => rec.Client)
                    .Include(rec => rec.Implementer)
                .Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.DateCreate.Date == model.DateCreate.Date) ||
                (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate.Date >= model.DateFrom.Value.Date && 
                rec.DateCreate.Date <= model.DateTo.Value.Date) ||
                (model.ClientId.HasValue && rec.ClientId == model.ClientId) ||
                (model.FreeOrders.HasValue && model.FreeOrders.Value && rec.Status == OrderStatus.Принят) ||
                (model.ImplementerId.HasValue && rec.ImplementerId == model.ImplementerId && rec.Status == OrderStatus.Выполняется))
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    AircraftName = rec.Aircraft.AircraftName,
                    AircraftId = rec.AircraftId,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    Status = rec.Status,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                    ClientId = rec.ClientId,
                    ClientName = rec.Client.ClientName,
                    ImplementerId = rec.ImplementerId,
                    ImplementerName = rec.ImplementerId.HasValue ? rec.Implementer.ImplementerName : string.Empty
                })
                .ToList();
            }
        }

        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new AbstractFactoryDatabase())
            {
                var order = context.Orders
                    .Include(rec => rec.Aircraft)
                    .Include(rec => rec.Client)
                    .Include(rec => rec.Implementer)
                .FirstOrDefault(rec => rec.Id == model.Id);
                return order != null ?
                new OrderViewModel
                {
                    Id = order.Id,
                    AircraftName = order.Aircraft.AircraftName,
                    AircraftId = order.AircraftId,
                    Count = order.Count,
                    Sum = order.Sum,
                    Status = order.Status,
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement,
                    ClientId = order.ClientId,
                    ClientName = order.Client.ClientName,
                    ImplementerId = order.ImplementerId,
                    ImplementerName = order.ImplementerId.HasValue ? order.Implementer.ImplementerName : string.Empty
                } :
                null;
            }
        }

        public void Insert(OrderBindingModel model)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                context.Orders.Add(CreateModel(model, new Order()));
                context.SaveChanges();
            }
        }

        public void Update(OrderBindingModel model)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                var element = context.Orders.FirstOrDefault(rec => rec.Id ==
                model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(OrderBindingModel model)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id ==
                model.Id);
                if (element != null)
                {
                    context.Orders.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.AircraftId = model.AircraftId;
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            order.ClientId = (int)model.ClientId;
            order.ImplementerId = model.ImplementerId;
            return order;
        }
    }
}
