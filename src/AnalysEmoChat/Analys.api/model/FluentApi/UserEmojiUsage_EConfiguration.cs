using Analys.api.model.user;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserEmojiUsage_EConfiguration : IEntityTypeConfiguration<UserEmojiUsage_E>
{
    public void Configure(EntityTypeBuilder<UserEmojiUsage_E> entity)
    {
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Emoji)
              .IsRequired()
              .HasMaxLength(64);

        entity.Property(e => e.Count)
              .HasDefaultValue(0);


        entity.Property(e => e.LastUpdated)
              .HasColumnType("TIMESTAMP")
              .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
