using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SSOAPI.Infra.Data.Models;

namespace SSOAPI.Infra.Data.Context
{
    public class SSOContext : IdentityDbContext<User>
    {
        public SSOContext(DbContextOptions<SSOContext> options)
        : base(options)
        {
            
        }

    }
}
