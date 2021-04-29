using System;
using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.Interfaces;
using AbstractAircraftFactoryLogic.ViewModels;
using AbstractFactoryFileImplement.Models;
using System.Collections.Generic;
using System.Linq;

namespace AbstractFactoryFileImplement.Implements
{
    public class StorageStorage : IStorageStorage
    {
        private readonly FileDataListSingleton source;

        public StorageStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<StorageViewModel> GetFullList()
        {
            return source.Storages.Select(CreateModel).ToList();
        }
        public List<StorageViewModel> GetFilteredList(StorageBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Storages.Where(rec => rec.StorageName.Contains(model.StorageName))
                 .Select(CreateModel).ToList();
        }
        public StorageViewModel GetElement(StorageBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var storages = source.Storages
                 .FirstOrDefault(rec => rec.StorageName == model.StorageName || rec.Id == model.Id);
            return storages != null ? CreateModel(storages) : null;
        }
        public void Insert(StorageBindingModel model)
        {
            int maxId = source.Storages.Count > 0 ? source.Components.Max(rec => rec.Id) : 0;
            var element = new Storage
            { 
                Id = maxId + 1,
                StorageComponents = new Dictionary<int, int>() 
            };
            source.Storages.Add(CreateModel(model, element));
        }
        public void Update(StorageBindingModel model)
        {
            var element = source.Storages.FirstOrDefault(rec => rec.Id == model.Id);
          
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }
        public void Delete(StorageBindingModel model)
        {
            Storage element = source.Storages.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null) {
                source.Storages.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private Storage CreateModel(StorageBindingModel model, Storage storage)
        {
            storage.StorageName = model.StorageName;
            storage.ResponsiblePerson = model.ResponsiblePerson;
            storage.DateCreate = model.DateCreate;
            // удаляем убранные
            foreach (var key in storage.StorageComponents.Keys.ToList())
            {
                if (!model.StorageComponents.ContainsKey(key))
                {
                    storage.StorageComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var component in model.StorageComponents)
            {
                if (storage.StorageComponents.ContainsKey(component.Key))
                {
                    storage.StorageComponents[component.Key] =
                    model.StorageComponents[component.Key].Item2;
                }
                else
                {
                    storage.StorageComponents.Add(component.Key,
                    model.StorageComponents[component.Key].Item2);
                }
            }
            return storage;
        }
       
        private StorageViewModel CreateModel(Storage storage)
        {
          
            return new StorageViewModel
            {
                Id = storage.Id,
                StorageName = storage.StorageName,
                ResponsiblePerson = storage.ResponsiblePerson,
                DateCreate = storage.DateCreate,
                StorageComponents = storage.StorageComponents
                    .ToDictionary(recPC => recPC.Key, recPC =>
                    (source.Components.FirstOrDefault(recC => recC.Id ==
                    recPC.Key)?.ComponentName, recPC.Value))
            };
        }
        public void Existence(StorageBindingModel houseBindingModel, int StorageId, int ComponentId, int Count, string ComponentName)
        {
            StorageViewModel view = GetElement(new StorageBindingModel
            {
                Id = StorageId
            });

            if (view != null)
            {
                houseBindingModel.StorageComponents = view.StorageComponents;
                houseBindingModel.DateCreate = view.DateCreate;
                houseBindingModel.Id = view.Id;
                houseBindingModel.ResponsiblePerson = view.ResponsiblePerson;
                houseBindingModel.StorageName = view.StorageName;
            }

            if (houseBindingModel.StorageComponents.ContainsKey(ComponentId))
            {
                int count = houseBindingModel.StorageComponents[ComponentId].Item2;
                houseBindingModel.StorageComponents[ComponentId] = (ComponentName, count + Count);
            }
            else
            {
                houseBindingModel.StorageComponents.Add(ComponentId, (ComponentName, Count));
            }
            Update(houseBindingModel);
        }
        public bool Extract(int AircraftCount, int AircraftId)
        {
            var list = GetFullList();

            var DCount = source.Aircrafts.FirstOrDefault(rec => rec.Id == AircraftId).AircraftComponents;

            DCount = DCount.ToDictionary(rec => rec.Key, rec => rec.Value * AircraftCount);

            Dictionary<int, int> Have = new Dictionary<int, int>();

            foreach (var view in list)
            {
                foreach (var d in view.StorageComponents)
                {
                    int key = d.Key;
                    if (DCount.ContainsKey(key))
                    {
                        if (Have.ContainsKey(key))
                        {
                            Have[key] += d.Value.Item2;
                        }
                        else
                        {
                            Have.Add(key, d.Value.Item2);
                        }
                    }
                }
            }

            foreach (var key in Have.Keys)
            {
                if (DCount[key] > Have[key])
                {
                    return false;
                }
            }

            foreach (var view in list)
            {
                var storageComponents = view.StorageComponents;
                foreach (var key in view.StorageComponents.Keys.ToArray())
                {
                    var value = view.StorageComponents[key];
                    if (DCount.ContainsKey(key))
                    {
                        if (value.Item2 > DCount[key])
                        {
                            storageComponents[key] = (value.Item1, value.Item2 - DCount[key]);
                            DCount[key] = 0;
                        }
                        else
                        {
                            storageComponents[key] = (value.Item1, 0);
                            DCount[key] -= value.Item2;
                        }
                        Update(new StorageBindingModel
                        {
                            Id = view.Id,
                            StorageName = view.StorageName,
                            ResponsiblePerson = view.ResponsiblePerson,
                            DateCreate = view.DateCreate,
                            StorageComponents = storageComponents
                        });
                    }
                }
            }
            return true;
        }
    }
}

