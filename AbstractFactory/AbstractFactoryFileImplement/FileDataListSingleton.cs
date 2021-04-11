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
        private readonly string StorageFileName = "Storage.xml";

        public List<Component> Components { get; set; }
		public List<Order> Orders { get; set; }
		public List<Aircraft> Aircrafts { get; set; }

        public List<Storage> Storages { get; set; }

        private FileDataListSingleton()
		{
			Components = LoadComponents();
			Orders = LoadOrders();
			Aircrafts = LoadAircrafts();
            Storages = LoadStorages();
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
            SaveStorages();
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
        private List<Storage> LoadStorages()
        {
            var list = new List<Storage>();
            if (File.Exists(StorageFileName))
            {
                XDocument xDocument = XDocument.Load(StorageFileName);
                var xElements = xDocument.Root.Elements("Storage").ToList();
                foreach (var elem in xElements)
                {
                    var storComp = new Dictionary<int, int>();
                    foreach (var component in
                    elem.Element("StorageComponents").Elements("WarehouseComponent").ToList())
                    {
                        storComp.Add(Convert.ToInt32(component.Element("Key").Value),
                        Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Storage
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        StorageName = elem.Element("StorageName").Value,
                        ResponsiblePerson = elem.Element("ResponsiblePerson").Value,
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        StorageComponents = storComp
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
        private void SaveStorages()
        {
            if (Storages != null)
            {
                var xElement = new XElement("Storages");
                foreach (var storage in Storages)
                {
                    var compElement = new XElement("StorageComponents");
                    foreach (var component in storage.StorageComponents)
                    {
                        compElement.Add(new XElement("StorageComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Warehouse",
                    new XAttribute("Id", storage.Id),
                    new XElement("StorageName", storage.StorageName),
                    new XElement("ResponsiblePerson", storage.ResponsiblePerson),
                    new XElement("DateCreate", storage.DateCreate),
                    compElement));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(StorageFileName);
            }
        }
    }
}