using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDateIdeas.Data.Identity
{
    public class FunDateIdeasIdentityDbContext : IdentityDbContext<FunDateIdeasUser>
    {
        public FunDateIdeasIdentityDbContext(DbContextOptions<FunDateIdeasIdentityDbContext> options): base(options)
        {

        }
    }
}
