using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SSOAPI.Domain.Core.Bus;
using SSOAPI.Domain.Core.Commands;
using SSOAPI.Domain.Core.Models;

namespace SSOAPI.Infra.Bus
{
    public sealed class Bus : IEventBus
    {

        private readonly IMediator _mediator;

        public Bus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<RequestResult> SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }
    }
}
