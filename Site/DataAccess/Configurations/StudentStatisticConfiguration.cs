using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Models.Entities;

namespace Site.DataAccess.Configurations
{
    public class StudentStatisticConfiguration : IEntityTypeConfiguration<StudentStatistic>
    {
        public void Configure(EntityTypeBuilder<StudentStatistic> builder)
        {
            builder.ToTable(nameof(StudentStatistic));

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
        }
    }
}
