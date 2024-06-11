using Services.B.Core.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Services.B.Core.Interfaces
{
    public interface IEventHandler<in TEvent> : IEventHandler
         where TEvent : Event
    {
        Task Handle(TEvent @event);
    }

    public interface IEventHandler
    {

    }
}
