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
		private readonly IAircraftStorage _aircraftStorage;
		private readonly IOrderStorage _orderStorage;
		private readonly IStorageStorage _storageStorage;

		public ReportLogic(IAircraftStorage aircraftStorage, IOrderStorage orderStorage, IStorageStorage storageStorage)
		{
			_aircraftStorage = aircraftStorage;
			_orderStorage = orderStorage;
			_storageStorage = storageStorage;
		}
		public List<ReportAircraftComponentViewModel> GetComponentsAircraft()
		{
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
				foreach (var component in aircraft.AircraftComponents)
				{
					record.Components.Add(new Tuple<string, int>(component.Value.Item1, component.Value.Item2));
					record.TotalCount += component.Value.Item2;
				}
				list.Add(record);
			}
			return list;
		}
		public List<ReportStorageComponentsViewModel> GetComponentsStorage()
		{
			var storages = _storageStorage.GetFullList();
			var listStorage = new List<ReportStorageComponentsViewModel>();
			foreach (var storage in storages)
			{
				var record = new ReportStorageComponentsViewModel
				{
					Name = storage.StorageName,
					Components = new List<Tuple<string, int>>(),
					Count = 0
				};
				foreach (var component in storage.StorageComponents)
				{
					record.Components.Add(new Tuple<string, int>(component.Value.Item1, component.Value.Item2));
					record.Count += component.Value.Item2;
				}
				listStorage.Add(record);
			}
			return listStorage;
		}
		public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
		{
			return _orderStorage.GetFilteredList(new OrderBindingModel
			{
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
		public List<ReportOrdersViewModel> GetOrdersGroupByDate()
		{
			return _orderStorage.GetFullList().GroupBy(x => x.DateCreate.Date)
			.Select(x => new ReportOrdersViewModel
			{
				DateCreate = x.Key,
				Count = x.Count(),
				Sum = x.Sum(rec => rec.Sum)
			}).ToList();
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
		public void SaveStorageToWordFile(ReportBindingModel model)
		{
			SaveToWord.CreateDocStorage(new WordInfoStorage
			{
				FileName = model.FileName,
				Title = "Список складов",
				Storages = _storageStorage.GetFullList()
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
		public void SaveStorageComponentToExcelFile(ReportBindingModel model)
		{
			SaveToExcel.CreateDocStorage(new ExcelInfoStorage
			{
				FileName = model.FileName,
				Title = "Список складов",
				StorageComponents = GetComponentsStorage()
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
		public void SaveAllOrdersToPdfFile(ReportBindingModel model)
		{
			SaveToPdf.CreateDocAllOrders(new PdfInfo
			{
				FileName = model.FileName,
				Title = "Список заказов",
				Orders = GetOrdersGroupByDate()
			});
		}
	}
}