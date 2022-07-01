using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Models.Entities;

namespace Site.DataAccess.Configurations
{
    public class EducationalMaterialConfiguration : IEntityTypeConfiguration<EducationalMaterial>
    {
        public void Configure(EntityTypeBuilder<EducationalMaterial> builder)
        {
            builder.ToTable(nameof(EducationalMaterial));

            builder.HasKey(sd => sd.Id);
            builder.Property(sd => sd.Id).ValueGeneratedOnAdd();
        }
    }
}
