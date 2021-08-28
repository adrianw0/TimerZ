using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimerZ.Domain.Models;

namespace TimerZ.DAL.EntityConfigurations
{
    public sealed class ProjectEntityConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects", "Timers");
            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.TimerEntries)
                .WithOne(te => te.Project)
                .HasForeignKey(te => te.ProjectId);
            builder.HasOne(p => p.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId);
        }
    }
}
