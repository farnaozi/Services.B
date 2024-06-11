using Services.B.Core.Events;
using Services.B.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.B.Core.Interfaces
{
    public interface IServiceRepo
    {
        Task ForwardMessage(string message);
    }
}
