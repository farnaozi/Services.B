using System;
using System.Threading.Tasks;

namespace Services.B.Core.Interfaces
{
    public interface ILoggerRepo
    {
        Task LogInfo(string message);
        Task LogError(string message);
        Task LogError(Exception exception);
    }
}
