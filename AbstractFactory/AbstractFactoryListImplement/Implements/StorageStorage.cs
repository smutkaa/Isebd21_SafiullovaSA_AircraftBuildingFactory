using System;
using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.Interfaces;
using AbstractAircraftFactoryLogic.ViewModels;
using AbstractFactoryListImplement.Models;
using System.Collections.Generic;
using System.Linq;

namespace AbstractFactoryListImplement.Implements
{
    public class StorageStorage : IStorageStorage
    {
        private readonly DataListSingleton source;

        public StorageStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<StorageViewModel> GetFullList()
        {
            List<StorageViewModel> result = new List<StorageViewModel>();
            foreach (var storage in source.Storages)
            {
                result.Add(CreateModel(storage));
            }
            return result;
        }
        public List<StorageViewModel> GetFilteredList(StorageBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<StorageViewModel> result = new List<StorageViewModel>();
            foreach (var storage in source.Storages)
            {
                if (storage.StorageName.Contains(model.StorageName))
                {
                    result.Add(CreateModel(storage));
                }
            }
            return result;
        }
        public StorageViewModel GetElement(StorageBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var storage in source.Storages)
            {
                if (storage.Id == model.Id || storage.StorageName == model.StorageName)
                {
                    return CreateModel(storage);
                }
            }
            return null;
        }
        public void Insert(StorageBindingModel model)
        {
            Storage tempStorage = new Storage { Id = 1, StorageComponents = new Dictionary<int, int>() };
            foreach (var storage in source.Storages)
            {
                if (storage.Id >= tempStorage.Id)
                {
                    tempStorage.Id = storage.Id + 1;
                }
            }
            source.Storages.Add(CreateModel(model, tempStorage));
        }
        public void Update(StorageBindingModel model)
        {
            Storage tempStorage = null;
            foreach (var storage in source.Storages)
            {
                if (storage.Id == model.Id)
                {
                    tempStorage = storage;
                }
            }
            if (tempStorage == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempStorage);
        }
        public void Delete(StorageBindingModel model)
        {
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == model.Id)
                {
                    source.Storages.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
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
            Dictionary<int, (string, int)> storageComponents = new Dictionary<int, (string, int)>();
            foreach (var pc in storage.StorageComponents)
            {
                string componentName = string.Empty;
                foreach (var component in source.Components)
                {
                    if (pc.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }
                }
                storageComponents.Add(pc.Key, (componentName, pc.Value));
            }
            return new StorageViewModel
            {
                Id = storage.Id,
                StorageName = storage.StorageName,
                ResponsiblePerson = storage.ResponsiblePerson,
                DateCreate = storage.DateCreate,
                StorageComponent = storageComponents
            };
        }
    }
}
