using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Site.Models.Entities;

namespace Site.DataAccess.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable(nameof(Group));

            builder.HasKey(sd => sd.Id);
            builder.Property(sd => sd.Id).ValueGeneratedOnAdd();


            builder.HasMany(sd => sd.Students)
                .WithOne(f => f.Group)
                .HasForeignKey(f => f.GroupId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(sd => sd.Courses)
                .WithOne(f => f.Group)
                .HasForeignKey(f => f.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
