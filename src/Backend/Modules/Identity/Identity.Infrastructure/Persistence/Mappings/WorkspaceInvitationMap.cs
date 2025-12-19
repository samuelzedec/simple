using BuildingBlocks.Infrastructure.Mapping;
using Identity.Domain.Entities;
using Identity.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Mappings;

public sealed class WorkspaceInvitationMap
    : BaseTypeConfiguration<WorkspaceInvitation>
{
    protected override string GetTableName()
        => "workspace_invitation";

    protected override void Mapping(EntityTypeBuilder<WorkspaceInvitation> builder)
    {
        builder
            .Property(wi => wi.WorkspaceId)
            .HasColumnName("workspace_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.OwnsOne(wi => wi.InviteeEmail, owned =>
        {
            owned
                .Property(ie => ie.Value)
                .HasColumnName("invitee_email")
                .HasColumnType("text")
                .HasMaxLength(Email.MaxLength)
                .IsRequired();

            owned.HasIndex(ie => ie.Value, "ix_workspace_invitation_invitee_email");
        });

        builder
            .Property(wi => wi.Role)
            .HasColumnName("role")
            .HasConversion<string>()
            .HasColumnType("text");

        builder
            .Property(wi => wi.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasColumnType("text");

        builder
            .Property(x => x.ExpiresAt)
            .HasColumnName("expires_at")
            .HasColumnType("timestamptz")
            .IsRequired();
    }
}