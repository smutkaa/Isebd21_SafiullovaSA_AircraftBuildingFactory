using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.HelperModels;
using AbstractAircraftFactoryLogic.Interfaces;
using AbstractAircraftFactoryLogic.ViewModels;
using AbstractAircraftFactoryLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractAircraftFactoryLogic.BusinessLogics
{
	public class ReportLogic
	{
		private readonly IComponentStorage _componentStorage;
		private readonly IAircraftStorage _aircraftStorage;
		private readonly IOrderStorage _orderStorage;

		public ReportLogic(IAircraftStorage aircraftStorage, IComponentStorage componentStorage, IOrderStorage orderStorage)
		{
			_aircraftStorage = aircraftStorage;
			_componentStorage = componentStorage;
			_orderStorage = orderStorage;
		}
		public List<ReportAircraftComponentViewModel> GetComponentsAircraft()
		{
			var components = _componentStorage.GetFullList();
			var aircrafts = _aircraftStorage.GetFullList();
			var list = new List<ReportAircraftComponentViewModel>();
			foreach (var aircraft in aircrafts)
			{
				var record = new ReportAircraftComponentViewModel
				{
					AircraftName = aircraft.AircraftName,
					Components = new List<Tuple<string, int>>(),
					TotalCount = 0
				};
				foreach (var component in components)
				{
					if (aircraft.AircraftComponents.ContainsKey(component.Id))
					{
						record.Components.Add(new Tuple<string, int>(component.ComponentName, aircraft.AircraftComponents[component.Id].Item2));
						record.TotalCount += aircraft.AircraftComponents[component.Id].Item2;
					}
				}
				list.Add(record);
			}
			return list;
		}
		public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
		{
			return _orderStorage.GetFilteredList(new OrderBindingModel {
				DateFrom = model.DateFrom, 
				DateTo = model.DateTo 
			})
			.Select(x => new ReportOrdersViewModel
			{
				DateCreate = x.DateCreate,
                AircraftName = x.AircraftName,
				Count = x.Count,
				Sum = x.Sum,
				Status = ((OrderStatus)Enum.Parse(typeof(OrderStatus), x.Status.ToString())).ToString()
			})
			.ToList();
		}
		public void SaveAircraftsToWordFile(ReportBindingModel model)
		{
			SaveToWord.CreateDoc(new WordInfo
			{
				FileName = model.FileName,
				Title = "Список изделий",
				Aircrafts = _aircraftStorage.GetFullList()
			});
		}
		public void SaveAircraftComponentToExcelFile(ReportBindingModel model)
		{
			SaveToExcel.CreateDoc(new ExcelInfo
			{
				FileName = model.FileName,
				Title = "Список изделий",
				AircraftComponents = GetComponentsAircraft()
			});
		}
		public void SaveOrdersToPdfFile(ReportBindingModel model)
		{
			SaveToPdf.CreateDoc(new PdfInfo
			{
				FileName = model.FileName,
				Title = "Список заказов",
				DateFrom = model.DateFrom.Value,
				DateTo = model.DateTo.Value,
				Orders = GetOrders(model)
			});
		}
	}
}