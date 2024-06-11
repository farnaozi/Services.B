using System.Collections.Generic;
using System.Threading.Tasks;
using Services.B.Core.Models;
using Services.B.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using Services.B.Core.Events;

namespace Services.B.Core.Repositories
{
    public class ServiceRepo : RepoBase, IServiceRepo
    {
        #region *** private

        private readonly IDBRepo _dbManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly IEventBus _bus;

        #endregion
        #region *** ctor

        public ServiceRepo(IDBRepo dbManager,
            IJwtFactory jwtFactory,
            IEventBus bus,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _jwtFactory = jwtFactory;
            _dbManager = dbManager;
            _bus = bus;
        }

        #endregion
        #region *** public

        public Task ForwardMessage(string message)
        {
            Console.WriteLine($"Message Received - {message}");
            _bus.Publish(new ServiceCDEvent()
            {
                Message = "deliver from service b to c and d"
            }, Enums.ExchangeTypes.Fanout, false);
            
            return Task.CompletedTask;
        }

        #endregion
    }
}