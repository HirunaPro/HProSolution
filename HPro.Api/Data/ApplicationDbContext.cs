using HPro.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HPro.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<ApplicationTag> ApplicationTags { get; set; }
        public DbSet<ObjectTag> ObjectTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationTag>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.ChildTags)
                      .WithOne(e => e.ParentTag)
                      .HasForeignKey(e => e.ParentTagId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ObjectTag>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.ApplicationTag)
                      .WithMany(e => e.ObjectTags)
                      .HasForeignKey(e => e.Id)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
