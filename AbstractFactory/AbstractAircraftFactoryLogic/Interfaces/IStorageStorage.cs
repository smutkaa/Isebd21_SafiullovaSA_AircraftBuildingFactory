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
        bool Unrestocking(int AircraftId, int Count);
    }
}
