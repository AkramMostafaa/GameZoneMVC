using GameZone.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameZone.Data.Configurations
{
    public class DeviceConfigurations : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.Property(D => D.Icon)
                 .HasMaxLength(250);

            builder.HasData(new Device[] 
            {
                    new Device{Id=1,Name="PlayStaion",Icon="bi bi-playstation" },
                    new Device{Id=2,Name="xBox",Icon="bi bi-xbox" },
                    new Device{Id=3,Name="Nintendo Switch",Icon="bi bi-nintendo-switch" },
                    new Device{Id=4,Name="PC",Icon="bi bi-pc-display" },
                    
            });
        }
    }
}
