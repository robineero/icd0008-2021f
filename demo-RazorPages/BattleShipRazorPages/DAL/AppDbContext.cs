using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        private const string Db = "hw05-razor";
        private static readonly string MssqlConnectionString = $"Server=vps.that.ee,1433;User Id=sa;Password=Robert.xnjj.1;Database={Db};MultipleActiveResultSets=true";
        private static readonly string PostgresqlConnectionString = $"Server=vps.that.ee;Port=5432;Database={Db};User Id=razor;Password=2q5t;";
        public DbSet<Game> Games { get; set; } = default!;
        public DbSet<Player> Players { get; set; } = default!;

        // not recommended - do not hardcode DB conf!
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(MssqlConnectionString);
            optionsBuilder.UseNpgsql(PostgresqlConnectionString);
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // disable cascade delete
            foreach (var relationship in builder.Model
                .GetEntityTypes()
                .Where(e => !e.IsOwned())
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}