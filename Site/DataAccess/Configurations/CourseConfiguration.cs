using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Models.Entities;

namespace Site.DataAccess.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable(nameof(Course));

            builder.HasKey(sd => sd.Id);
            builder.Property(sd => sd.Id).ValueGeneratedOnAdd();


            builder.HasMany(sd => sd.Students)
                .WithOne(f => f.Course)
                .HasForeignKey(f => f.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(sd => sd.Materials)
                .WithOne(f => f.Course)
                .HasForeignKey(f => f.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
