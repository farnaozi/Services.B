using FluentValidation;
using Services.B.Core.Events;
using Services.B.Core.Handlers;
using Services.B.Core.Interfaces;
using Services.B.Core.Models;
using Services.B.Core.Repositories;
using Services.B.Infrastructure;
using Services.B.Infrastructure.DB;
using Services.B.Infrastructure.Logger;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using Microsoft.Extensions.Options;

namespace Services.B.Core.Helpers
{
    public static class ServiceExtension
    {
        [Conditional("DEBUG")]
        static void AddDebugModeRepositories(IServiceCollection services)
        {
            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                var appSettings = sp.GetRequiredService<IOptions<AppSettings>>();
                return new RabbitMQBus(scopeFactory, appSettings);
            });
            services.AddSingleton<ServiceBEventHandler>();
            services.AddSingleton<IEventHandler<ServiceBEvent>, ServiceBEventHandler>();
            services.AddSingleton<IDBRepo, DBRepo>();
            services.AddSingleton<IServiceRepo, ServiceRepo>();
            services.AddSingleton<ILoggerRepo, LoggerRepo>();
            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<ServiceExceptions>();
        }

        [Conditional("RELEASE")]
        static void AddReleaseModeRepositories(IServiceCollection services)
        {
            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                var appSettings = sp.GetRequiredService<IOptions<AppSettings>>();
                return new RabbitMQBus(scopeFactory, appSettings);
            });
            services.AddSingleton<ServiceBEventHandler>();
            services.AddSingleton<IEventHandler<ServiceBEvent>, ServiceBEventHandler>();
            services.AddSingleton<IDBRepo, DBRepo>();
            services.AddSingleton<IServiceRepo, ServiceRepo>();
            services.AddSingleton<ILoggerRepo, LoggerRepo>();
            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<ServiceExceptions>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            AddDebugModeRepositories(services);
            AddReleaseModeRepositories(services);
        }
    }
}
