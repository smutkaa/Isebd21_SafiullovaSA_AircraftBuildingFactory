using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.Enums;
using AbstractAircraftFactoryLogic.Interfaces;
using AbstractAircraftFactoryLogic.ViewModels;
using System;
using System.Collections.Generic;


namespace AbstractAircraftFactoryLogic.BusinessLogics
{
    public class StorageLogic 
    {
        private readonly IStorageStorage _storageStorage;
        public StorageLogic(IStorageStorage storageStorage)
        {
            _storageStorage = storageStorage;
        }
        public List<StorageViewModel> Read(StorageBindingModel model)
        {
            if (model == null)
            {
                return _storageStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<StorageViewModel> { _storageStorage.GetElement(model) };
            }
            return _storageStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(StorageBindingModel model)
        {
            var element = _storageStorage.GetElement(new StorageBindingModel { StorageName = model.StorageName });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            if (model.Id.HasValue)
            {
                _storageStorage.Update(model);
            }
            else
            {
                _storageStorage.Insert(model);
            }
        }
        public void Delete(StorageBindingModel model)
        {
            var storage = _storageStorage.GetElement(new StorageBindingModel { Id = model.Id });
            if (storage == null)
            {
                throw new Exception("Склад не найден");
            }
            _storageStorage.Delete(model);
        }
    }
}
