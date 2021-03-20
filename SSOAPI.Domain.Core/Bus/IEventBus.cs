using System.Threading.Tasks;
using SSOAPI.Domain.Core.Commands;
using SSOAPI.Domain.Core.Models;

namespace SSOAPI.Domain.Core.Bus
{
    public interface IEventBus
    {
        Task<RequestResult> SendCommand<T>(T command) where T : Command;
    }
}
