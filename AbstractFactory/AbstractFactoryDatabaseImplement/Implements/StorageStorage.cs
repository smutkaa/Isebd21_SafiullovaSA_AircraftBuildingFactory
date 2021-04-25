using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.Interfaces;
using AbstractAircraftFactoryLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;
using AbsctractFactoryDatabaseImplement.Models;

namespace AbstractFactoryDatabaseImplement.Implements
{
    public class StorageStorage : IStorageStorage
    {
        public List<StorageViewModel> GetFullList()
        {
            using (var context = new AbstractFactoryDatabase())
            {
                return context.Storages
                .Include(rec => rec.StorageComponent)
                .ThenInclude(rec => rec.Component)
                .ToList()
                .Select(rec => new StorageViewModel
                {
                    Id = rec.Id,
                    StorageName = rec.StorageName,
                    ResponsiblePerson = rec.ResponsiblePerson,
                    DateCreate = rec.DateCreate,
                    StorageComponents = rec.StorageComponent
                .ToDictionary(recPC => recPC.ComponentId, recPC =>
                (recPC.Component?.ComponentName, recPC.Count))
                })
                .ToList();
            }
        }

        public List<StorageViewModel> GetFilteredList(StorageBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new AbstractFactoryDatabase())
            {
                return context.Storages
                .Include(rec => rec.StorageComponent)
                .ThenInclude(rec => rec.Component)
                .Where(rec => rec.StorageName.Contains(model.StorageName))
                .ToList()
                .Select(rec => new StorageViewModel
                {
                    Id = rec.Id,
                    StorageName = rec.StorageName,
                    ResponsiblePerson = rec.ResponsiblePerson,
                    DateCreate = rec.DateCreate,
                    StorageComponents = rec.StorageComponent
                .ToDictionary(recPC => recPC.ComponentId, recPC =>
                (recPC.Component?.ComponentName, recPC.Count))
                })
                .ToList();
            }
        }

        public StorageViewModel GetElement(StorageBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new AbstractFactoryDatabase())
            {
                var storage = context.Storages
                .Include(rec => rec.StorageComponent)
                .ThenInclude(rec => rec.Component)
                .FirstOrDefault(rec => rec.StorageName.Equals(model.StorageName) || rec.Id
                == model.Id);
                return storage != null ?
                new StorageViewModel
                {
                    Id = storage.Id,
                    StorageName = storage.StorageName,
                    ResponsiblePerson = storage.ResponsiblePerson,
                    DateCreate = storage.DateCreate,
                    StorageComponents = storage.StorageComponent
                .ToDictionary(recPC => recPC.ComponentId, recPC =>
                (recPC.Component?.ComponentName, recPC.Count))
                } : null;
            }
        }

        public void Insert(StorageBindingModel model)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Storage storage = CreateModel(model, new Storage());
                        context.Storages.Add(storage);
                        context.SaveChanges();
                        CreateModel(model, storage, context);

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

        public void Update(StorageBindingModel model)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Storages.FirstOrDefault(rec => rec.Id == model.Id);
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

        public void Delete(StorageBindingModel model)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                Storage element = context.Storages.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Storages.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        private Storage CreateModel(StorageBindingModel model, Storage storage)
        {
            storage.StorageName = model.StorageName;
            storage.ResponsiblePerson = model.ResponsiblePerson;
            storage.DateCreate = model.DateCreate;
            return storage;
        }

        private Storage CreateModel(StorageBindingModel model, Storage storage,
        AbstractFactoryDatabase context)
        {
            storage.StorageName = model.StorageName;
            storage.ResponsiblePerson = model.ResponsiblePerson;
            storage.DateCreate = model.DateCreate;
            if (model.Id.HasValue)
            {
                var aircraftComponents = context.StorageComponents.Where(rec =>
                rec.StorageId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.StorageComponents.RemoveRange(aircraftComponents.Where(rec =>
                !model.StorageComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateComponent in aircraftComponents)
                {
                    updateComponent.Count =
                    model.StorageComponents[updateComponent.ComponentId].Item2;
                    model.StorageComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var pc in model.StorageComponents)
            {
                context.StorageComponents.Add(new StorageComponent
                {
                    StorageId = storage.Id,
                    ComponentId = pc.Key,
                    Count = pc.Value.Item2,
                });
                try
                {
                    context.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
            return storage;
        }

        public bool Unrestocking(int AircraftId, int Count)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                var list = GetFullList();
                var DCount = context.AircraftComponents.Where(rec => rec.AircraftId == AircraftId)
                    .ToDictionary(rec => rec.ComponentId, rec => rec.Count * Count);

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var key in DCount.Keys.ToArray())
                        {
                            foreach (var storageComponent in context.StorageComponents.Where(rec => rec.ComponentId == key))
                            {
                                if (storageComponent.Count > DCount[key])
                                {
                                    storageComponent.Count -= DCount[key];
                                    DCount[key] = 0;
                                    break;
                                }
                                else
                                {
                                    DCount[key] -= storageComponent.Count;
                                    storageComponent.Count = 0;
                                }
                            }
                            if (DCount[key] > 0)
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                        context.SaveChanges();
                        transaction.Commit();

                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}