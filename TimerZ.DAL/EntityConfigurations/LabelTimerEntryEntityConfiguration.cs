using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimerZ.Domain.Models;

namespace TimerZ.DAL.EntityConfigurations
{
    public class LabelTimerEntryEntityConfiguration : IEntityTypeConfiguration<LabelTimerEntry>
    {
        public void Configure(EntityTypeBuilder<LabelTimerEntry> builder)
        {

        }
    }
}
