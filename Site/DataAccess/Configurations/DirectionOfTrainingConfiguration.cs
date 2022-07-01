using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Models.Entities;

namespace Site.DataAccess.Configurations
{
    public class DirectionOfTrainingConfiguration : IEntityTypeConfiguration<DirectionOfTraining>
    {
        public void Configure(EntityTypeBuilder<DirectionOfTraining> builder)
        {
            builder.ToTable(nameof(DirectionOfTraining));

            builder.HasKey(sd => sd.Id);
            builder.Property(sd => sd.Id).ValueGeneratedOnAdd();


            builder.HasMany(sd => sd.Groups)
                .WithOne(f => f.DirectionOfTraining)
                .HasForeignKey(f => f.DirectionOfTrainingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
