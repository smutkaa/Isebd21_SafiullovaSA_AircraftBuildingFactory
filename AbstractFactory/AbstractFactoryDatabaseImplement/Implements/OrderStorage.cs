﻿using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.Interfaces;
using AbstractAircraftFactoryLogic.ViewModels;
using AbstractFactoryDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AbstractFactoryDatabaseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using (var context = new AbstractFactoryDatabase())
            {
                return context.Orders.Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    AircraftName = context.Aircrafts
                    .Include(x => x.Order)
                    .FirstOrDefault(x => x.Id == rec.AircraftId).AircraftName,
                    AircraftId = rec.AircraftId,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    Status = rec.Status,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement
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
            if (model.DateFrom != null && model.DateTo != null)
            {
                using (var context = new AbstractFactoryDatabase())
                {
                    return context.Orders.Where(rec => rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo)
                        .Select(rec => new OrderViewModel
                        {
                            Id = rec.Id,
                            AircraftName = context.Aircrafts
                            .Include(x => x.Order)
                            .FirstOrDefault(x => x.Id == rec.AircraftId).AircraftName,
                            AircraftId = rec.AircraftId,
                            Count = rec.Count,
                            Sum = rec.Sum,
                            Status = rec.Status,
                            DateCreate = rec.DateCreate,
                            DateImplement = rec.DateImplement
                        }).ToList();
                }
            }
             
            using (var context = new AbstractFactoryDatabase())
            {
                return context.Orders
                .Where(rec => rec.Id.Equals(model.Id))
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    AircraftName = context.Aircrafts
                    .Include(x => x.Order)
                    .FirstOrDefault(r => r.Id == rec.AircraftId).AircraftName,
                    AircraftId = rec.AircraftId,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    Status = rec.Status,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement
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
                .FirstOrDefault(rec => rec.Id == model.Id);
                return order != null ?
                new OrderViewModel
                {
                    Id = order.Id,
                    AircraftName = context.Aircrafts.FirstOrDefault(r => r.Id == order.AircraftId).AircraftName,
                    AircraftId = order.AircraftId,
                    Count = order.Count,
                    Sum = order.Sum,
                    Status = order.Status,
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement
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
            return order;
        }
    }
}
