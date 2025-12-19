using BuildingBlocks.Domain.ValueObjects;
using BuildingBlocks.Infrastructure.Mapping;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Mappings;

public sealed class WorkspaceMap 
    : BaseTypeConfiguration<Workspace>
{
    protected override string GetTableName()
        => "workspace";

    protected override void Mapping(EntityTypeBuilder<Workspace> builder)
    {
        builder.OwnsOne(w => w.Name, owned => owned
            .Property(f => f.Value)
            .HasColumnName("name")
            .HasColumnType("text")
            .HasMaxLength(Name.MaxLength)
            .IsRequired()
        );

        builder
            .Property(w => w.IsActive)
            .HasColumnName("is_active")
            .HasColumnType("boolean")
            .IsRequired();

        builder
            .Property(w => w.MaxUsers)
            .HasColumnName("max_users")
            .HasColumnType("integer")
            .IsRequired();

        builder.Ignore("_userWorkspaces");
    }
}