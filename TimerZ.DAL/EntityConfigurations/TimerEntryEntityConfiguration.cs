using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimerZ.Domain.Models;

namespace TimerZ.DAL.EntityConfigurations
{
    public sealed class TimerEntryEntityConfiguration : IEntityTypeConfiguration<TimerEntry>
    {
        public void Configure(EntityTypeBuilder<TimerEntry> builder)
        {
            builder.HasKey(te => te.Id);
            builder.ToTable("TimerEntries", "Timers");
            builder.HasMany(te => te.Labels)
                .WithMany(l => l.TimerEntries);
            builder.HasOne(te => te.Project)
                .WithMany(p => p.TimerEntries)
                .HasForeignKey(te => te.ProjectId);
            builder.HasOne(te => te.User)
                .WithMany(u=>u.TimerEntries)
                .HasForeignKey(te=>te.UserId);



        }
    }
}
