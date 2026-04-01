using BudgetTracker.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetTracker.Infrastructure.ApplicationDbContext
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(u => u.Budget)
                .WithOne(b => b.User)
                .HasForeignKey<Budget>(u => u.UserId);

                entity.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(u => u.Login)
                .HasMaxLength(50)
                .IsRequired();

                entity.Property(u => u.PasswordHash)
                .HasMaxLength(60)
                .IsRequired();
            });


            modelBuilder.Entity<Budget>(entity =>
            {
                entity.HasMany(b => b.Transactions)
                .WithOne(t => t.Budget)
                .HasForeignKey(b => b.BudgetId);

                entity.Property(b => b.Balance)
                .HasPrecision(18, 2);
            });
                

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId);

                entity.Property(t => t.Amount)
                .HasPrecision(18,2)
                .IsRequired();

                entity.Property( t => t.Description)
                .HasMaxLength(500);
            });

            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);
                
        }
    }
}
