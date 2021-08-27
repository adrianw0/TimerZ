using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimerZ.Domain.Models;

namespace TimerZ.DAL.EntityConfigurations
{
    public sealed class LabelEntityConfiguration : IEntityTypeConfiguration<Label>
    {
        public void Configure(EntityTypeBuilder<Label> builder)
        {
            builder.ToTable("Labels", "Timers");
            builder.HasMany(l => l.TimerEntries)
                .WithMany(te => te.Labels);
        }
    }
}
