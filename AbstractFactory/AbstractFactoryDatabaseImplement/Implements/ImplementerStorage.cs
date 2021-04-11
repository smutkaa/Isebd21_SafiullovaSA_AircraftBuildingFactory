using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.Interfaces;
using AbstractAircraftFactoryLogic.ViewModels;
using AbstractFactoryDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryDatabaseImplement.Implements
{
    public class ImplementerStorage : IImplementerStorage
    {
        public List<ImplementerViewModel> GetFullList()
        {
            using (var context = new AbstractFactoryDatabase())
            {
                return context.Implementers.Select(rec => new ImplementerViewModel
                {
                    Id = rec.Id,
                    ImplementerName = rec.ImplementerName,
                    WorkingTime = rec.WorkingTime,
                    PauseTime = rec.PauseTime
                })
                .ToList();
            }
        }

        public List<ImplementerViewModel> GetFilteredList(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new AbstractFactoryDatabase())
            {
                return context.Implementers
                .Where(rec => rec.ImplementerName  == model.ImplementerName)
                .Select(rec => new ImplementerViewModel
                {
                    Id = rec.Id,
                    ImplementerName = rec.ImplementerName,
                    WorkingTime = rec.WorkingTime,
                    PauseTime = rec.PauseTime

                })
                .ToList();
            }
        }

        public ImplementerViewModel GetElement(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new AbstractFactoryDatabase())
            {
                var implementer = context.Implementers
                .FirstOrDefault(rec => rec.Id == model.Id);
                return implementer != null ?
                new ImplementerViewModel
                {
                    Id = implementer.Id,
                    ImplementerName = implementer.ImplementerName,
                    WorkingTime = implementer.WorkingTime,
                    PauseTime = implementer.PauseTime

                } :
                null;
            }
        }

        public void Insert(ImplementerBindingModel model)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                context.Implementers.Add(CreateModel(model, new Implementer(), context));
                context.SaveChanges();
            }
        }

        public void Update(ImplementerBindingModel model)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                var element = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Клиент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
            }
        }

        public void Delete(ImplementerBindingModel model)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                Implementer element = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Implementers.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Клиент не найден");
                }
            }
        }

        private Implementer CreateModel(ImplementerBindingModel model, Implementer implementer, AbstractFactoryDatabase database)
        {
            implementer.ImplementerName = model.ImplementerName;
            implementer.WorkingTime = model.WorkingTime;
            implementer.PauseTime = model.PauseTime;

            return implementer;
        }
    }
}
