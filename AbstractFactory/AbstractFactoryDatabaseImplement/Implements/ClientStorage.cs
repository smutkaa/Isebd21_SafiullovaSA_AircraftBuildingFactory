using System;
using System.Collections.Generic;
using System.Linq;
using AbstractAircraftFactoryLogic.Interfaces;
using AbstractAircraftFactoryLogic.ViewModels;
using AbstractAircraftFactoryLogic.BindingModels;
using AbstractFactoryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace AbstractFactoryDatabaseImplement.Implements
{
    public class ClientStorage : IClientStorage
    {
        public List<ClientViewModel> GetFullList()
        {
            using (var context = new AbstractFactoryDatabase())
            {
                return context.Clients.Select(rec => new ClientViewModel
                {
                    Id = rec.Id,
                    ClientName = rec.ClientName,
                    Login = rec.Login,
                    Password = rec.Password,
                })
                .ToList();
            }
        }

        public List<ClientViewModel> GetFilteredList(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new AbstractFactoryDatabase())
            {
                return context.Clients.Include(x => x.Order)
                .Where(rec => rec.Login == model.Login && rec.Password == rec.Password)
                .Select(rec => new ClientViewModel
                {
                    Id = rec.Id,
                    ClientName = rec.ClientName,
                    Login = rec.Login,
                    Password = rec.Password,
                })
                .ToList();
            }
        }

        public ClientViewModel GetElement(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new AbstractFactoryDatabase())
            {
                var client = context.Clients.Include(x => x.Order)
                .FirstOrDefault(rec => rec.Login == model.Login ||
                rec.Id == model.Id);
                return client != null ?
                new ClientViewModel
                {
                    Id = client.Id,
                    ClientName = client.ClientName,
                    Login = client.Login,
                    Password = client.Password,
                } :
                null;
            }
        }

        public void Insert(ClientBindingModel model)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                context.Clients.Add(CreateModel(model, new Client(), context));
                context.SaveChanges();
            }
        }

        public void Update(ClientBindingModel model)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                var element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Клиент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
            }
        }

        public void Delete(ClientBindingModel model)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                Client element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Clients.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Клиент не найден");
                }
            }
        }

        private Client CreateModel(ClientBindingModel model, Client client, AbstractFactoryDatabase database)
        {
            client.ClientName = model.ClientName;
            client.Login = model.Login;
            client.Password = model.Password;
            return client;
        }
    }
}
