using Services.B.Core.Models;

namespace Services.B.Core.Events
{
    public class ServiceBEvent : Event
    {
        public string Message { get; set; }
    }
}
