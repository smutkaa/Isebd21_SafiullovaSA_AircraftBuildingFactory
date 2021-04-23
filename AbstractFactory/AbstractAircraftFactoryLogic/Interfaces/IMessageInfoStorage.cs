using System;
using System.Collections.Generic;
using System.Text;
using AbstractAircraftFactoryLogic.ViewModels;
using AbstractAircraftFactoryLogic.BindingModels;

namespace AbstractAircraftFactoryLogic.Interfaces
{
    public interface IMessageInfoStorage
    {
        List<MessageInfoViewModel> GetFullList();
        List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model);
        void Insert(MessageInfoBindingModel model);
    }
}
