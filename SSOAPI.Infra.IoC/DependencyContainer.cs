using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SSOAPI.Application.Interfaces;
using SSOAPI.Application.Services;
using SSOAPI.Domain.CommandHandler;
using SSOAPI.Domain.Commands;
using SSOAPI.Domain.Core.Bus;
using SSOAPI.Domain.Core.Models;
using SSOAPI.Infra.Data.Context;

namespace SSOAPI.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IEventBus, Bus.Bus>();

            //Commands
            services.AddTransient<IRequestHandler<RegistrarNovoUserCommand, RequestResult>, UserCommandHandler>();
            services.AddTransient<IRequestHandler<AlterarSenhaUserCommand, RequestResult>, UserCommandHandler>();

            services.AddTransient<IUserAppService, UserAppService>();
            

        }
    }
}
