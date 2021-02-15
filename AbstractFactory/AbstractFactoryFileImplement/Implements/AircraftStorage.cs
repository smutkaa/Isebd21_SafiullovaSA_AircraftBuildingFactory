using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.Interfaces;
using AbstractAircraftFactoryLogic.ViewModels;
using AbstractFactoryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractFactoryFileImplement.Implements
{
	public class AircraftStorage : IAircraftStorage
	{
		private readonly FileDataListSingleton source;

		public AircraftStorage()
		{
			source = FileDataListSingleton.GetInstance();
		}

		public List<AircraftViewModel> GetFullList()
		{
			return source.Aircrafts
			.Select(CreateModel)
			.ToList();
		}
		public List<AircraftViewModel> GetFilteredList(AircraftBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			return source.Aircrafts
			.Where(rec => rec.AircraftName.Contains(model.AircraftName))
			.Select(CreateModel)
			.ToList();
		}
		public AircraftViewModel GetElement(AircraftBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			var aircraft = source.Aircrafts
			.FirstOrDefault(rec => rec.AircraftName == model.AircraftName || rec.Id == model.Id);
			return aircraft != null ? CreateModel(aircraft) : null;
		}
		public void Insert(AircraftBindingModel model)
		{
			int maxId = source.Aircrafts.Count > 0 ? source.Components.Max(rec => rec.Id) : 0;
			var element = new Aircraft { Id = maxId + 1, AircraftComponents = new Dictionary<int, int>() };
			source.Aircrafts.Add(CreateModel(model, element));
		}
		public void Update(AircraftBindingModel model)
		{
			var element = source.Aircrafts.FirstOrDefault(rec => rec.Id == model.Id);
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			CreateModel(model, element);
		}
		public void Delete(AircraftBindingModel model)
		{
			Aircraft element = source.Aircrafts.FirstOrDefault(rec => rec.Id == model.Id);
			if (element != null)
			{
				source.Aircrafts.Remove(element);
			}
			else
			{
				throw new Exception("Элемент не найден");
			}
		}

		private Aircraft CreateModel(AircraftBindingModel model, Aircraft aircraft)
		{
			aircraft.AircraftName = model.AircraftName;
			aircraft.Price = model.Price;
			// удаляем убранные
			foreach (var key in aircraft.AircraftComponents.Keys.ToList())
			{
				if (!model.AircraftComponents.ContainsKey(key))
				{
					aircraft.AircraftComponents.Remove(key);
				}
			}
			// обновляем существуюущие и добавляем новые
			foreach (var component in model.AircraftComponents)
			{
				if (aircraft.AircraftComponents.ContainsKey(component.Key))
				{
					aircraft.AircraftComponents[component.Key] = model.AircraftComponents[component.Key].Item2;
				}
				else
				{
					aircraft.AircraftComponents.Add(component.Key, model.AircraftComponents[component.Key].Item2);
				}
			}
			return aircraft;
		}
		private AircraftViewModel CreateModel(Aircraft aircraft)
		{
			return new AircraftViewModel
			{
				Id = aircraft.Id,
				AircraftName = aircraft.AircraftName,
				Price = aircraft.Price,
				AircraftComponents = aircraft.AircraftComponents.ToDictionary(recPC => recPC.Key, recPC =>
				(source.Components.FirstOrDefault(recC => recC.Id == recPC.Key)?.ComponentName, recPC.Value))
			};
		}
	}
}
