using System;
using System.Collections.Generic;
using System.Text;

namespace SSOAPI.Domain.Commands
{
    public class RegistrarNovoUserCommand : UserCommand
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
