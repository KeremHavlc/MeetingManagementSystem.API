using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Persistence.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeetingManagementSystem.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<AppUser , AppRole , Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Decision> Decisions { get; set; }
        public DbSet<DecisionAssignment> DecisionAssignments { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingParticipant> MeetingParticipants { get; set; }
        public DbSet<MeetingRole> MeetingRole { get; set; }
        public DbSet<ChatMessage> ChatMessage { get; set; }
        public DbSet<MeetingInvite> MeetingInvites { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Identity Tablolarını yeniden adlandırma
            modelBuilder.Entity<AppUser>(b => b.ToTable("Users"));
            modelBuilder.Entity<AppRole>(b => b.ToTable("Roles"));

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

            //AppUser -> DecisionAssignment (1:N)
            modelBuilder.Entity<DecisionAssignment>()
                .HasOne(ap => ap.AppUser)
                .WithMany(da => da.DecisionAssignments)
                .HasForeignKey(ap => ap.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //AppUser -> MeetingParticipant (1:N)
            modelBuilder.Entity<MeetingParticipant>()
                .HasOne(ap => ap.AppUser)
                .WithMany(mp => mp.MeetingParticipants)
                .HasForeignKey(ap => ap.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //AppUser -> Meeting (1:N)
            modelBuilder.Entity<Meeting>()
                .HasOne(ap => ap.AppUser)
                .WithMany(me => me.Meetings)
                .HasForeignKey(ap => ap.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
            //User silinse bile meeting kalsın demek ---> Restrict

            //MeetingRole -> MeetingParticipant (1:N)
            modelBuilder.Entity<MeetingParticipant>()
                .HasOne(mr => mr.MeetingRole)
                .WithMany(mr => mr.MeetingParticipants)
                .HasForeignKey(mr => mr.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            //MeetingInvite -> Meeting (1:N)
            modelBuilder.Entity<MeetingInvite>()
                .HasOne(mi => mi.Meeting)
                .WithMany(m => m.MeetingInvites) 
                .HasForeignKey(mi => mi.MeetingId)
                .OnDelete(DeleteBehavior.Cascade);

            //ChatMessage Entity Rules            
            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.ToTable("ChatMessages");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Message)
                      .IsRequired()
                      .HasMaxLength(2000);

                entity.Property(x => x.CreatedAt)
                      .HasDefaultValueSql("GETUTCDATE()");
            });

            //UserSetting -> AppUser (1:1)
            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.UserSettings)
                .WithOne(s => s.AppUser)
                .HasForeignKey<UserSettings>(s => s.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            //Seed Data Configuration
            modelBuilder.ApplyConfiguration(new MeetingRoleConfiguration());

            //Identity Kütüphanesinde kullanılmayacak tabloların kaldırılması
            modelBuilder.Ignore<IdentityUserLogin<Guid>>();
            modelBuilder.Ignore<IdentityUserToken<Guid>>();
            modelBuilder.Ignore<IdentityUserClaim<Guid>>();
            modelBuilder.Ignore<IdentityRoleClaim<Guid>>();
            modelBuilder.Ignore<IdentityUserRole<Guid>>();
                                
        }
    }
}
