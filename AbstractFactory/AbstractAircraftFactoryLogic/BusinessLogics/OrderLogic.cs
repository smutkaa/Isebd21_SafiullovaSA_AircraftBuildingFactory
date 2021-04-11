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
		private readonly object locker = new object();
		private readonly IOrderStorage _orderStorage;
		public OrderLogic(IOrderStorage orderStorage)
		{
			_orderStorage = orderStorage;
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
				Status = OrderStatus.Принят,
				ClientId = model.ClientId
			});
		}
		public void TakeOrderInWork(ChangeStatusBindingModel model)
		{
			lock (locker)
			{

				var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
				if (order == null)
				{
					throw new Exception("Не найден заказ");
				}
				if (order.Status != OrderStatus.Принят)
				{
					throw new Exception("Заказ не в статусе \"Принят\"");
				}
				if (order.ImplementerId.HasValue)
				{
					throw new Exception("У заказа уже есть исполнитель");
				}
				_orderStorage.Update(new OrderBindingModel
				{
					Id = order.Id,
					AircraftId = order.AircraftId,
					Count = order.Count,
					Sum = order.Sum,
					DateCreate = order.DateCreate,
					DateImplement = DateTime.Now,
					Status = OrderStatus.Выполняется,
					ClientId = order.ClientId,
					ImplementerId = model.ImplementerId,
				});
			}
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
				Status = OrderStatus.Готов,
				ClientId = order.ClientId
			});
		}
		public void PayOrder(ChangeStatusBindingModel model)
		{
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
                Status = OrderStatus.Оплачен,
				ClientId = order.ClientId
			});
        }
	}
}
