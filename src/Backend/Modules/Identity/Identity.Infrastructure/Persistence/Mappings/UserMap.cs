using BuildingBlocks.Infrastructure.Mapping;
using Identity.Domain.Entities;
using Identity.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Mappings;

public sealed class UserMap 
    : BaseTypeConfiguration<User>
{
    protected override string GetTableName()
        => "user";

    protected override void Mapping(EntityTypeBuilder<User> builder)
    {
        builder.OwnsOne(u => u.FullName, owned => owned
            .Property(f => f.Value)
            .HasColumnName("full_name")
            .HasColumnType("text")
            .HasMaxLength(FullName.MaxLength)
            .IsRequired()
        );

        builder.OwnsOne(u => u.Email, owned =>
        {
            owned
                .Property(e => e.Value)
                .HasColumnName("email")
                .HasColumnType("text")
                .HasMaxLength(Email.MaxLength)
                .IsRequired();

            owned
                .HasIndex(e => e.Value, "uq_user_email")
                .IsUnique();
        });

        builder
            .Property(u => u.ApplicationUserId)
            .HasColumnName("application_user_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder
            .HasIndex(u => u.ApplicationUserId, "uq_user_application_user_id")
            .IsUnique();

        builder.Ignore("_userWorkspaces");
    }
}