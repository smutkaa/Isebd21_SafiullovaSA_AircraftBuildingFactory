using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.ViewModels;
using System.Collections.Generic;

namespace AbstractAircraftFactoryLogic.Interfaces
{
    public interface IStorageStorage
    {
        List<StorageViewModel> GetFullList();
        List<StorageViewModel> GetFilteredList(StorageBindingModel model);
        StorageViewModel GetElement(StorageBindingModel model);
        void Insert(StorageBindingModel model);
        void Update(StorageBindingModel model);
        void Delete(StorageBindingModel model);
        void Availability(StorageBindingModel houseBindingModel, int StorageId, int ComponentId, int Count, string ComponentName);
        bool Extract(int AircraftCount, int AircraftId);
    }
}
