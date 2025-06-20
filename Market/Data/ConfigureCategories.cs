using Market.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Data
{
    public class ConfigureCategories : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
