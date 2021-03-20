using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SSOAPI.Infra.Data.Models
{
    public class User : IdentityUser
    {
        public string Nome { get; set; }
    }
}
