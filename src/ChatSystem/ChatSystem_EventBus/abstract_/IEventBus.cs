using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem_EventBus.abstract_
{
    public interface IEventBus
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : Event;
    }

}
