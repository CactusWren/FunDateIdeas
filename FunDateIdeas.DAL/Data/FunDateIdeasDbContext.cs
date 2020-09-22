using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDateIdeas.DAL.Data
{
    public class FunDateIdeasDbContext : DbContext
    {
        public FunDateIdeasDbContext(DbContextOptions<FunDateIdeasDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DateIdeaLike>().HasKey(table => new {
                table.DateIdeaId,
                table.UserId
            });
        }
        public DbSet<DateIdea> DateIdeas { get; set; }
        public DbSet<DateIdeaLike> DateIdeaLikes { get; set; }
    }
}
