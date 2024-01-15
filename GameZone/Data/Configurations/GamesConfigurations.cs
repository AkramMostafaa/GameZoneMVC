using GameZone.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameZone.Data.Configurations
{
    public class GamesConfigurations : IEntityTypeConfiguration<Games>
    {
        public void Configure(EntityTypeBuilder<Games> builder)
        {
            builder.Property(G => G.Description)
                .HasMaxLength(2500);

            builder.Property(G => G.Cover)
                .HasMaxLength(2500);
        }
    }
}
