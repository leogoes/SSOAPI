using System;
using SSOAPI.Domain.Core.Events;

namespace SSOAPI.Domain.Core.Commands
{
    public abstract class Command : Message
    {
        public DateTime TimeStamp { get; set; }

        protected Command()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
