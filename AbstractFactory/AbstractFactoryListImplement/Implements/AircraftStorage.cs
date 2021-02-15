using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractAircraftFactoryLogic.Interfaces;
using AbstractAircraftFactoryLogic.ViewModels;
using AbstractAircraftFactoryLogic.BindingModels;
using AbstractFactoryListImplement.Models;

namespace AbstractFactoryListImplement.Implements
{
    public class AircraftStorage : IAircraftStorage
    {
        private readonly DataListSingleton source;
        public AircraftStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<AircraftViewModel> GetFullList()
        {
            List<AircraftViewModel> result = new List<AircraftViewModel>();
            foreach (var component in source.Aircraft)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }
        public List<AircraftViewModel> GetFilteredList(AircraftBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<AircraftViewModel> result = new List<AircraftViewModel>();
            foreach (var aircraft in source.Aircraft)
            {
                if (aircraft.AircraftName.Contains(model.AircraftName))
                {
                    result.Add(CreateModel(aircraft));
                }
            }
            return result;
        }
        public AircraftViewModel GetElement(AircraftBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var aircraft in source.Aircraft)
            {
                if (aircraft.Id == model.Id || aircraft.AircraftName == model.AircraftName)
                {
                    return CreateModel(aircraft);
                }
            }
            return null;
        }
        public void Insert(AircraftBindingModel model)
        {
            Aircraft tempAircraft = new Aircraft
            {
                Id = 1,
                AircraftComponents = new
            Dictionary<int, int>()
            };
            foreach (var aircraft in source.Aircraft)
            {
                if (aircraft.Id >= tempAircraft.Id)
                {
                    tempAircraft.Id = aircraft.Id + 1;
                }
            }
            source.Aircraft.Add(CreateModel(model, tempAircraft));
        }
        public void Update(AircraftBindingModel model)
        {
            Aircraft tempAircraft = null;
            foreach (var aircraft in source.Aircraft)
            {
                if (aircraft.Id == model.Id)
                {
                    tempAircraft = aircraft;
                }
            }
            if (tempAircraft == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempAircraft);
        }
        public void Delete(AircraftBindingModel model)
        {
            for (int i = 0; i < source.Aircraft.Count; ++i)
            {
                if (source.Aircraft[i].Id == model.Id)
                {
                    source.Aircraft.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
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
                    aircraft.AircraftComponents[component.Key] =
                    model.AircraftComponents[component.Key].Item2;
                }
                else
                {
                    aircraft.AircraftComponents.Add(component.Key,
                    model.AircraftComponents[component.Key].Item2);
                }
            }
            return aircraft;
        }
        private AircraftViewModel CreateModel(Aircraft aircraft)
        {
            // требуется дополнительно получить список компонентов для изделия с названиями и их количество
        Dictionary<int, (string, int)> aircraftComponents = new
        Dictionary<int, (string, int)>();
            foreach (var pc in aircraft.AircraftComponents)
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
                aircraftComponents.Add(pc.Key, (componentName, pc.Value));
            }
            return new AircraftViewModel
            {
                Id = aircraft.Id,
                AircraftName = aircraft.AircraftName,
                Price = aircraft.Price,
                AircraftComponents = aircraftComponents
            };
        }
    }
}
