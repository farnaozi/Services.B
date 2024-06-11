using Services.B.Core.Models;

namespace Services.B.Core.Events
{
    public class ServiceCDEvent : Event
    {
        public string Message { get; set; }
    }
}
