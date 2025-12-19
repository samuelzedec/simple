using BuildingBlocks.Infrastructure.Mapping;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Mappings;

public sealed class UserWorkspaceMap 
    : BaseTypeConfiguration<UserWorkspace>
{
    protected override string GetTableName()
        => "user_workspace";

    protected override void Mapping(EntityTypeBuilder<UserWorkspace> builder)
    {
        builder
            .Property(uw => uw.UserId)
            .HasColumnName("user_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder
            .Property(uw => uw.WorkspaceId)
            .HasColumnName("workspace_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder
            .HasIndex(uw => new { uw.UserId, uw.WorkspaceId }, "uq_user_workspace_user_id_workspace_id")
            .IsUnique();

        builder
            .Property(uw => uw.Role)
            .HasColumnName("role")
            .HasConversion<string>()
            .HasColumnType("text");

        builder
            .Property(uw => uw.IsActive)
            .HasColumnName("is_active")
            .HasColumnType("boolean")
            .IsRequired();

        builder
            .HasOne(uw => uw.User)
            .WithMany(u => u.UserWorkspaces)
            .HasForeignKey(uw => uw.UserId)
            .HasConstraintName("fk_user_workspace_user_id")
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(uw => uw.Workspace)
            .WithMany(w => w.UserWorkspaces)
            .HasConstraintName("fk_user_workspace_workspace_id")
            .HasForeignKey(uw => uw.WorkspaceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}