using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestoranMVCMPA201.Models;

namespace RestoranMVCMPA201.Configurations;

public class FoodConfiguration : IEntityTypeConfiguration<Food>
{
    public void Configure(EntityTypeBuilder<Food> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(512);
        builder.Property(x => x.ImagePath).IsRequired().HasMaxLength(512);
        builder.HasOne(x => x.Category).WithMany(x => x.Foods).HasForeignKey(x => x.CategoryId).HasPrincipalKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);
    }
}
