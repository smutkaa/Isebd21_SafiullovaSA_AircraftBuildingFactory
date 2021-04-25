using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.Interfaces;
using AbstractAircraftFactoryLogic.ViewModels;
using AbstractFactoryDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;

namespace AbstractFactoryDatabaseImplement.Implements
{
    public class AircraftStorage : IAircraftStorage
    {
        public List<AircraftViewModel> GetFullList()
        {
            using (var context = new AbstractFactoryDatabase())
            {
                return context.Aircrafts
                .Include(rec => rec.AircraftComponent)
               .ThenInclude(rec => rec.Component)
               .ToList()
               .Select(rec => new AircraftViewModel
               {
                   Id = rec.Id,
                   AircraftName = rec.AircraftName,
                   Price = rec.Price,
                   AircraftComponents = rec.AircraftComponent.ToDictionary(recPC => recPC.ComponentId, recPC =>
                  (recPC.Component?.ComponentName, recPC.Count))
               })
               .ToList();
            }
        }
        public List<AircraftViewModel> GetFilteredList(AircraftBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new AbstractFactoryDatabase())
            {
                return context.Aircrafts
                .Include(rec => rec.AircraftComponent)
               .ThenInclude(rec => rec.Component)
               .Where(rec => rec.AircraftName.Contains(model.AircraftName))
               .ToList()
               .Select(rec => new AircraftViewModel
               {
                   Id = rec.Id,
                   AircraftName = rec.AircraftName,
                   Price = rec.Price,
                   AircraftComponents = rec.AircraftComponent
                .ToDictionary(recPC => recPC.ComponentId, recPC =>

                   (recPC.Component?.ComponentName, recPC.Count))
               })
               .ToList();
            }
        }
        public AircraftViewModel GetElement(AircraftBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new AbstractFactoryDatabase())
            {
                var aircraft = context.Aircrafts
                .Include(rec => rec.AircraftComponent)
               .ThenInclude(rec => rec.Component)
               .FirstOrDefault(rec => rec.AircraftName == model.AircraftName || rec.Id
               == model.Id);
                return aircraft != null ?
                new AircraftViewModel
                {
                    Id = aircraft.Id,
                    AircraftName = aircraft.AircraftName,
                    Price = aircraft.Price,
                    AircraftComponents = aircraft.AircraftComponent
                .ToDictionary(recPC => recPC.ComponentId, recPC =>
               (recPC.Component?.ComponentName, recPC.Count))
                } :
               null;
            }
        }
        public void Insert(AircraftBindingModel model)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Aircraft aircraft = CreateModel(model, new Aircraft());
                        context.Aircrafts.Add(aircraft);
                        context.SaveChanges();
                        aircraft = CreateModel(model, aircraft, context);

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Update(AircraftBindingModel model)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Aircrafts.FirstOrDefault(rec => rec.Id ==
                       model.Id);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        CreateModel(model, element, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(AircraftBindingModel model)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                Aircraft element = context.Aircrafts.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Aircrafts.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Aircraft CreateModel(AircraftBindingModel model, Aircraft aircraft)
        {
            aircraft.AircraftName = model.AircraftName;
            aircraft.Price = model.Price;
            return aircraft;
        }
        private Aircraft CreateModel(AircraftBindingModel model, Aircraft aircraft,
       AbstractFactoryDatabase context)
        {
            aircraft.AircraftName = model.AircraftName;
            aircraft.Price = model.Price;

            if (model.Id.HasValue)
            {
                var aircraftComponents = context.AircraftComponents.Where(rec =>
               rec.AircraftId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.AircraftComponents.RemoveRange(aircraftComponents.Where(rec =>
               !model.AircraftComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateComponent in aircraftComponents)
                {
                    updateComponent.Count =
                   model.AircraftComponents[updateComponent.ComponentId].Item2;
                    model.AircraftComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }

            // добавили новые
            foreach (var pc in model.AircraftComponents)
            {
                context.AircraftComponents.Add(new AircraftComponent
                {
                    AircraftId = aircraft.Id,
                    ComponentId = pc.Key,
                    Count = pc.Value.Item2
                });
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    MessageBox.Show(e?.InnerException?.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
            return aircraft;
        }
    }
}