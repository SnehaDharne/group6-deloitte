using System;
using Microsoft.EntityFrameworkCore;
using Deloitte_Project.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Deloitte_Project.Models
{
    public partial class CoreDbContext : DbContext
    {
        public CoreDbContext()
        {
        }
        
        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-2D9NDJF\\SQLEXPRESS;Database=user-management;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
