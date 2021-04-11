using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.Enums;
using AbstractAircraftFactoryLogic.Interfaces;
using AbstractAircraftFactoryLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace AbstractAircraftFactoryLogic.BusinessLogics
{
	public class OrderLogic
	{
		private readonly IOrderStorage _orderStorage;
        private readonly IStorageStorage _storageStorage;
        public OrderLogic(IOrderStorage orderStorage, IStorageStorage storageStorage)
		{
			_orderStorage = orderStorage;
            _storageStorage = storageStorage;
        }
		public List<OrderViewModel> Read(OrderBindingModel model)
		{
			if (model == null)
			{
				return _orderStorage.GetFullList();
			}
			if (model.Id.HasValue)
			{
				return new List<OrderViewModel> { _orderStorage.GetElement(model) };
			}
			return _orderStorage.GetFilteredList(model);
		}
		public void CreateOrder(CreateOrderBindingModel model)
		{
			_orderStorage.Insert(new OrderBindingModel
			{
                AircraftId = model.AircraftId,
				Count = model.Count,
				Sum = model.Sum,
				DateCreate = DateTime.Now,
				Status = OrderStatus.Принят
			});
		}
		public void TakeOrderInWork(ChangeStatusBindingModel model)
		{
			var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
			if (order == null)
			{
				throw new Exception("Не найден заказ");
			}
			if (!_storageStorage.Extract(order.Count, order.AircraftId))
			{
				throw new Exception("Компонентов не достаточно");
			}
			if (order == null)
			{
				throw new Exception("Не найден заказ");
			}
			_orderStorage.Update(new OrderBindingModel
			{
				Id = order.Id,
                AircraftId = order.AircraftId,
				Count = order.Count,
				Sum = order.Sum,
				DateCreate = order.DateCreate,
				DateImplement = DateTime.Now,
				Status = OrderStatus.Выполняется
			});
		}
		public void FinishOrder(ChangeStatusBindingModel model)
		{
			var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
			if (order == null)
			{
				throw new Exception("Не найден заказ");
			}
			if (order.Status != OrderStatus.Выполняется)
			{
				throw new Exception("Заказ не в статусе \"Выполняется\"");
			}
			_orderStorage.Update(new OrderBindingModel
			{
				Id = order.Id,
                AircraftId = order.AircraftId,
				Count = order.Count,
				Sum = order.Sum,
				DateCreate = order.DateCreate,
				DateImplement = order.DateImplement,
				Status = OrderStatus.Готов
			});
		}
		public void PayOrder(ChangeStatusBindingModel model)
		{
            // продумать логику
            var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                AircraftId = order.AircraftId,
				Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Оплачен
            });
        }
	}
}
