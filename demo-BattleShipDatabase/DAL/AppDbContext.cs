﻿using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        private static string ConnectionString =
            "Server=vps.that.ee,1433;User Id=sa;Password=Robert.xnjj.1;Database=hw04;MultipleActiveResultSets=true";
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Post> Posts { get; set; } = default!;
        public DbSet<PostCategory> PostCategories { get; set; } = default!;

        // not recommended - do not hardcode DB conf!
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
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