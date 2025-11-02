using MeetingManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetingManagementSystem.Persistence.Seed
{
    public class MeetingRoleConfiguration : IEntityTypeConfiguration<MeetingRole>
    {
        public void Configure(EntityTypeBuilder<MeetingRole> builder)
        {
            builder.HasData(
                new MeetingRole
                {
                    Id = Guid.Parse("4168731e-174c-4613-8689-8b864b687c06"),
                    RoleName = "Admin",
                    IsActive = true,
                    CreatedAt = DateTime.Parse("2025-11-01T14:42:14.0138554"),
                    UpdatedAt = DateTime.MinValue
                },
                new MeetingRole
                {
                    Id = Guid.Parse("7e652bde-6dd2-4b7f-bd7f-db51ac8ece98"),
                    RoleName = "Moderator",
                    IsActive = true,
                    CreatedAt = DateTime.Parse("2025-11-01T14:57:54.9078576"),
                    UpdatedAt = DateTime.MinValue
                },
                new MeetingRole
                {
                    Id = Guid.Parse("62eb21cb-bd81-4094-b83a-8019555e581d"),
                    RoleName = "Participant",
                    IsActive = true,
                    CreatedAt = DateTime.Parse("2025-11-01T14:58:10.3993242"),
                    UpdatedAt = DateTime.MinValue
                }
                );
        }
    }
}
