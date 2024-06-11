using Services.B.Core.Events;
using Services.B.Core.Interfaces;
using System.Threading.Tasks;

namespace Services.B.Core.Handlers
{
    public class ServiceBEventHandler : IEventHandler<ServiceBEvent>
    {
        private readonly IServiceRepo _serviceRepo;

        public ServiceBEventHandler(IServiceRepo serviceRepo)
        {
            _serviceRepo = serviceRepo;
        }

        public Task Handle(ServiceBEvent @event)
        {
            _serviceRepo.ForwardMessage(@event.Message);
            return Task.CompletedTask;
        }
    }
}
