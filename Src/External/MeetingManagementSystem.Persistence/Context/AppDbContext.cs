using MeetingManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingManagementSystem.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Decision> Decisions { get; set; }
        public DbSet<DecisionAssignment> DecisionAssignments { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingParticipant> MeetingParticipants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Meeting -> Decision (1:N)
            modelBuilder.Entity<Decision>()
                .HasOne(d => d.Meeting)
                .WithMany(m => m.Decisions)
                .HasForeignKey(d => d.MeetingId)
                .OnDelete(DeleteBehavior.Cascade);

            //Meeting -> MeetingParticipant (1:N)
            modelBuilder.Entity<MeetingParticipant>()
                .HasOne(mp => mp.Meeting)
                .WithMany(m => m.MeetingParticipants)
                .HasForeignKey(mp => mp.MeetingId)
                .OnDelete(DeleteBehavior.Cascade);

            //Decision -> DecisionAssignment (1:N)
            modelBuilder.Entity<DecisionAssignment>()
                .HasOne(da => da.Decision)
                .WithMany(d => d.DecisionAssignments)
                .HasForeignKey(da => da.DecisionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
