using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Models.Entities;

namespace Site.DataAccess.Configurations
{
    public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
    {
        public void Configure(EntityTypeBuilder<Faculty> builder)
        {
            builder.ToTable(nameof(Faculty));

            builder.HasKey(sd => sd.Id);
            builder.Property(sd => sd.Id).ValueGeneratedOnAdd();


            builder.HasMany(sd => sd.DirectionOfTrainings)
                .WithOne(f => f.Faculty)
                .HasForeignKey(f => f.FacultyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
