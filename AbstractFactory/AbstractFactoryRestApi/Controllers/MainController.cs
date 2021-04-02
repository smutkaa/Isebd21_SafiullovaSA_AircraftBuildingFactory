using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.BusinessLogics;
using AbstractAircraftFactoryLogic.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AbstractFactoryRestApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class MainController : ControllerBase
	{
		private readonly OrderLogic _order;
		private readonly AircraftLogic _aircraft;
		private readonly OrderLogic _main;
		public MainController(OrderLogic order, AircraftLogic aircraft, OrderLogic main)
		{
			_order = order;
			_aircraft = aircraft;
			_main = main;
		}
		[HttpGet]
		public List<AircraftViewModel> GetAircraftList() => _aircraft.Read(null)?.ToList();
		[HttpGet]
		public AircraftViewModel GetAircraft(int aircraftId) => _aircraft.Read(new AircraftBindingModel { Id = aircraftId })?[0];
		[HttpGet]
		public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel { ClientId = clientId });
		[HttpPost]
		public void CreateOrder(CreateOrderBindingModel model) => _main.CreateOrder(model);
	}
}
