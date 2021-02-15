using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.Interfaces;
using AbstractAircraftFactoryLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace AbstractAircraftFactoryLogic.BusinessLogics
{
	public class AircraftLogic
	{
		private readonly IAircraftStorage _aircraftStorage;
		public AircraftLogic(IAircraftStorage aircraftStorage)
		{
			_aircraftStorage = aircraftStorage;
		}
		public List<AircraftViewModel> Read(AircraftBindingModel model)
		{
			if (model == null)
			{
				return _aircraftStorage.GetFullList();
			}
			if (model.Id.HasValue)
			{
				return new List<AircraftViewModel> { _aircraftStorage.GetElement(model) };
			}
			return _aircraftStorage.GetFilteredList(model);
		}
		public void CreateOrUpdate(AircraftBindingModel model)
		{
			var element = _aircraftStorage.GetElement(new AircraftBindingModel { AircraftName = model.AircraftName });
			if (element != null && element.Id != model.Id)
			{
				throw new Exception("Уже есть компонент с таким названием");
			}
			if (model.Id.HasValue)
			{
				_aircraftStorage.Update(model);
			}
			else
			{
				_aircraftStorage.Insert(model);
			}
		}
		public void Delete(AircraftBindingModel model)
		{
			var element = _aircraftStorage.GetElement(new AircraftBindingModel { Id = model.Id });
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			_aircraftStorage.Delete(model);
		}
	}
}
