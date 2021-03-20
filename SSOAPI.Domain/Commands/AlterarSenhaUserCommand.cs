using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace SSOAPI.Domain.Commands
{
    public class AlterarSenhaUserCommand : UserCommand
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
