using System;
using System.Collections.Generic;
using System.Text;
using AbstractAircraftFactoryLogic.Enums;
using AbstractFactoryFileImplement.Models;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AbstractFactoryFileImplement
{
	class FileDataListSingleton
	{
		private static FileDataListSingleton instance;

		private readonly string ComponentFileName = "Component.xml";
		private readonly string OrderFileName = "Order.xml";
		private readonly string AircraftFileName = "Aircraft.xml";
		private readonly string ClientFileName = "Client.xml";
		private readonly string ImplementerFileName = "Implementer.xml";
		private readonly string MessageInfoFileName = "MessageInfo.xml";

		public List<Component> Components { get; set; }
		public List<Order> Orders { get; set; }
		public List<Aircraft> Aircrafts { get; set; }
		public List<Client> Clients { get; set; }
		public List<Implementer> Implementers { get; set; }
		public List<MessageInfo> MessageInfoes { get; set; }

		private FileDataListSingleton()
		{
			Components = LoadComponents();
			Orders = LoadOrders();
			Aircrafts = LoadAircrafts();
			Clients = LoadClients();
			Implementers = LoadImplementer();
			MessageInfoes = LoadMessageInfo();
		}
		public static FileDataListSingleton GetInstance()
		{
			if (instance == null)
			{
				instance = new FileDataListSingleton();
			}
			return instance;
		}

		~FileDataListSingleton()
		{
			SaveComponents();
			SaveOrders();
			SaveAircrafts();
			SaveClients();
			SaveImplementer();
			SaveMessageInfo();
		}

		private List<Component> LoadComponents()
		{
			var list = new List<Component>();
			if (File.Exists(ComponentFileName))
			{
				XDocument xDocument = XDocument.Load(ComponentFileName);
				var xElements = xDocument.Root.Elements("Component").ToList();
				foreach (var elem in xElements)
				{
					list.Add(new Component
					{
						Id = Convert.ToInt32(elem.Attribute("Id").Value),
						ComponentName = elem.Element("ComponentName").Value
					});
				}
			}
			return list;
		}
		private List<Order> LoadOrders()
		{
			var list = new List<Order>();
			if (File.Exists(OrderFileName))
			{
				XDocument xDocument = XDocument.Load(OrderFileName);
				var xElements = xDocument.Root.Elements("Order").ToList();

				foreach (var elem in xElements)
				{
					OrderStatus status = 0;
					switch (elem.Element("Status").Value)
					{
						case "Принят":
							status = OrderStatus.Принят;
							break;
						case "Выполняется":
							status = OrderStatus.Выполняется;
							break;
						case "Готов":
							status = OrderStatus.Готов;
							break;
						case "Оплачен":
							status = OrderStatus.Оплачен;
							break;
					}

					list.Add(new Order
					{
						Id = Convert.ToInt32(elem.Attribute("Id").Value),
						AircraftId = Convert.ToInt32(elem.Element("AircraftId").Value),
						Count = Convert.ToInt32(elem.Element("Count").Value),
						Sum = Convert.ToDecimal(elem.Element("Sum").Value),
						Status = status,
						DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
						DateImplement = string.IsNullOrEmpty(elem.Element("DateImplement").Value) ? (DateTime?)null :
						Convert.ToDateTime(elem.Element("DateImplement").Value)
					});
				}
			}
			return list;
		}
		private List<Aircraft> LoadAircrafts()
		{
			var list = new List<Aircraft>();
			
			if (File.Exists(AircraftFileName))
			{
				XDocument xDocument = XDocument.Load(AircraftFileName);
				var xElements = xDocument.Root.Elements("Aircraft").ToList();
				foreach (var elem in xElements)
				{
					var prodComp = new Dictionary<int, int>();
					foreach (var component in elem.Element("AircraftComponents").Elements("AircraftComponent").ToList())
					{
						prodComp.Add(Convert.ToInt32(component.Element("Key").Value), Convert.ToInt32(component.Element("Value").Value));
					}
					list.Add(new Aircraft
					{
						Id = Convert.ToInt32(elem.Attribute("Id").Value),
						AircraftName = elem.Element("AircraftName").Value,
						Price = Convert.ToDecimal(elem.Element("Price").Value),
						AircraftComponents = prodComp
					});
				}
			}
			return list;
		}
		private List<Client> LoadClients()
		{
			var list = new List<Client>();
			if (File.Exists(ClientFileName))
			{
				XDocument xDocument = XDocument.Load(ClientFileName);
				var xElements = xDocument.Root.Elements("Client").ToList();
				foreach (var elem in xElements)
				{
					list.Add(new Client
					{
						Id = Convert.ToInt32(elem.Attribute("Id").Value),
						ClientName = elem.Element("ClientName").Value,
						Login = elem.Element("Login").Value,
						Password = elem.Element("Password").Value,
					});
				}
			}
			return list;
		}
		private List<Implementer> LoadImplementer()
		{
			var list = new List<Implementer>();
			if (File.Exists(ImplementerFileName))
			{
				XDocument xDocument = XDocument.Load(ImplementerFileName);
				var xElements = xDocument.Root.Elements("Implementer").ToList();
				foreach (var elem in xElements)
				{
					list.Add(new Implementer
					{
						Id = Convert.ToInt32(elem.Attribute("Id").Value),
						ImplementerName = elem.Element("ImplementerName").Value,
						WorkingTime = Convert.ToInt32(elem.Element("WorkingTime").Value),
						PauseTime = Convert.ToInt32(elem.Element("PauseTime").Value)
					});
				}
			}
			return list;
		}
		private List<MessageInfo> LoadMessageInfo()
		{
			var list = new List<MessageInfo>();
			if (File.Exists(MessageInfoFileName))
			{
				XDocument xDocument = XDocument.Load(MessageInfoFileName);
				var xElements = xDocument.Root.Elements("MessageInfo").ToList();

				foreach (var elem in xElements)
				{
					list.Add(new MessageInfo
					{
						MessageId = elem.Attribute("Id").Value,
						ClientId = Convert.ToInt32(elem.Element("ClientId").Value),
						SenderName = elem.Element("ClientId").Value,
						Subject = elem.Element("Subject").Value,
						Body = elem.Element("Body").Value,
						DateDelivery = Convert.ToDateTime(elem.Element("DateDelivery").Value)
						PauseTime = Convert.ToInt32(elem.Element("PauseTime").Value),
					});
				}
			}
			return list;
		}
		private void SaveComponents()
		{
			if (Components != null)
			{
				var xElement = new XElement("Components");
				foreach (var component in Components)
				{
					xElement.Add(new XElement("Component",
					new XAttribute("Id", component.Id),
					new XElement("ComponentName", component.ComponentName)));
					XDocument xDocument = new XDocument(xElement);
					xDocument.Save(ComponentFileName);
				}
			}
		}
		private void SaveOrders()
		{
			if (Orders != null)
			{
				var xElement = new XElement("Orders");
				foreach (var order in Orders)
				{
					xElement.Add(new XElement("Order",
					new XAttribute("Id", order.Id),
					new XElement("AircraftId", order.AircraftId),
					new XElement("Count", order.Count),
					new XElement("Sum", order.Sum),
					new XElement("Status", order.Status),
					new XElement("DateCreate", order.DateCreate),
					new XElement("DateImplement", order.DateImplement)));

				}
				XDocument xDocument = new XDocument(xElement);
				xDocument.Save(OrderFileName);
			}
		}
		private void SaveAircrafts()
		{
			if (Aircrafts != null)
			{
				var xElement = new XElement("Aircrafts");
				foreach (var aircraft in Aircrafts)
				{
					var compElement = new XElement("AircraftComponents");
					foreach (var component in aircraft.AircraftComponents)
					{
						compElement.Add(new XElement("AircraftComponent",
						new XElement("Key", component.Key),
						new XElement("Value", component.Value)));
					}
					xElement.Add(new XElement("Aircraft",
					new XAttribute("Id", aircraft.Id),
					new XElement("AircraftName", aircraft.AircraftName),
					new XElement("Price", aircraft.Price),
					compElement));
				}
				XDocument xDocument = new XDocument(xElement);
				xDocument.Save(AircraftFileName);
			}
		}
		private void SaveClients()
		{
			if (Clients != null)
			{
				var xElement = new XElement("Clients");
				foreach (var client in Clients)
				{
					xElement.Add(new XElement("Client",
					new XAttribute("Id", client.Id),
					new XElement("ClientName", client.ClientName),
					new XElement("Login", client.Login),
					new XElement("Password", client.Password)));
				}
				XDocument xDocument = new XDocument(xElement);
				xDocument.Save(ClientFileName);
			}
		}
		private void SaveImplementer()
		{
			if (Implementers != null)
			{
				var xElement = new XElement("Implementers");
				foreach (var implementer in Implementers)
				{
					xElement.Add(new XElement("Implementer",
					new XAttribute("Id", implementer.Id),
					new XElement("ImplementerName", implementer.ImplementerName),
					new XElement("WorkingTime", implementer.WorkingTime),
					new XElement("PauseTime", implementer.PauseTime)));
				}
				XDocument xDocument = new XDocument(xElement);
				xDocument.Save(ImplementerFileName);
			}
		}
		private void SaveMessageInfo()
		{
			if (MessageInfoes != null)
			{
				var xElement = new XElement("MessageInfo");
				foreach (var messageInfo in MessageInfoes)
				{
					xElement.Add(new XElement("MessageInfo",
					new XAttribute("MessageId", messageInfo.MessageId),
					new XElement("Subject", messageInfo.Subject),
					new XElement("SenderName", messageInfo.SenderName),
					new XElement("Body", messageInfo.Body),
					new XElement("ClientId", messageInfo.ClientId),
					new XElement("DateDelivery", messageInfo.DateDelivery)));
				}
				XDocument xDocument = new XDocument(xElement);
				xDocument.Save(MessageInfoFileName);
			}
		}
	}
}