using MediatR;
using SSOAPI.Domain.Core.Models;

namespace SSOAPI.Domain.Core.Events
{
    public abstract class Message : IRequest<RequestResult>
    {
        public string MessageType { get; set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
