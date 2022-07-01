using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Models.Entities;

namespace Site.DataAccess.Configurations
{
    public class StructuralDivisionConfiguration : IEntityTypeConfiguration<StructuralDivision>
    {
        public void Configure(EntityTypeBuilder<StructuralDivision> builder)
        {
            builder.ToTable(nameof(StructuralDivision));

            builder.HasKey(sd => sd.Id);
            builder.Property(sd => sd.Id).ValueGeneratedOnAdd();

            builder.HasMany(sd => sd.Faculties)
                .WithOne(f => f.StructuralDivision)
                .HasForeignKey(f => f.StructuralDivisionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(sd => sd.DirectionOfTrainings)
                .WithOne(f => f.StructuralDivision)
                .HasForeignKey(f => f.StructuralDivisionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
