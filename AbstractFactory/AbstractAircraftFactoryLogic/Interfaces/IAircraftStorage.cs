using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.ViewModels;
using System.Collections.Generic;

namespace AbstractAircraftFactoryLogic.Interfaces
{
	public interface IAircraftStorage
	{
		List<AircraftViewModel> GetFullList();
		List<AircraftViewModel> GetFilteredList(AircraftBindingModel model);
		AircraftViewModel GetElement(AircraftBindingModel model);
		void Insert(AircraftBindingModel model);
		void Update(AircraftBindingModel model);
		void Delete(AircraftBindingModel model);
	}
}
