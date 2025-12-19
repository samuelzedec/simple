using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Domain.Exceptions;
using Identity.Domain.Enums;
using Identity.Domain.Resources;
using Identity.Domain.ValueObjects;

namespace Identity.Domain.Entities;

/// <summary>
/// Representa um convite enviado para um usuário ingressar em um <see cref="Workspace"/>.
/// </summary>
/// <remarks>
/// Um convite possui o email do usuário convidado, o papel que ele terá no workspace,
/// o status do convite e a data de expiração. Convites são criados como pendentes
/// e não podem ser alterados, apenas apagados ou expirados.
/// </remarks>
public sealed class WorkspaceInvitation : BaseEntity
{
    /// <summary>
    /// Número máximo de dias que um convite pode expirar.
    /// </summary>
    public const int MaxDays = 7;

    /// <summary>
    /// Identificador do workspace relacionado ao convite.
    /// </summary>
    public Guid WorkspaceId { get; init; }

    /// <summary>
    /// Email do usuário que está sendo convidado.
    /// </summary>
    public Email InviteeEmail { get; init; } = null!;

    /// <summary>
    /// Papel que será atribuído ao usuário convidado no workspace.
    /// </summary>
    public UserRole Role { get; init; }

    /// <summary>
    /// Status atual do convite.
    /// </summary>
    public InvitationStatus Status { get; init; }

    /// <summary>
    /// Data e hora de expiração do convite.
    /// </summary>
    public DateTime ExpiresAt { get; init; }

    private WorkspaceInvitation() { }

    private WorkspaceInvitation(Guid workspaceId, Email inviteeEmail, UserRole role, DateTime expiresAt)
    {
        WorkspaceId = workspaceId;
        InviteeEmail = inviteeEmail;
        Role = role;
        Status = InvitationStatus.Pending;
        ExpiresAt = expiresAt;
    }

    /// <summary>
    /// Cria uma nova instância de <see cref="WorkspaceInvitation"/>.
    /// </summary>
    /// <param name="workspaceId">Identificador do workspace que está enviando o convite.</param>
    /// <param name="inviteeEmail">Email do usuário que será convidado.</param>
    /// <param name="role">Papel que o usuário terá ao aceitar o convite.</param>
    /// <param name="expiresAt">Número de dias até o convite expirar.</param>
    /// <returns>Uma instância válida de <see cref="WorkspaceInvitation"/>.</returns>
    /// <exception cref="DomainException">
    /// Lançada quando o <paramref name="workspaceId"/> é inválido ou
    /// quando o número de dias de expiração excede o limite máximo.
    /// </exception>
    public static WorkspaceInvitation Create(Guid workspaceId, string inviteeEmail, UserRole role, int expiresAt)
    {
        if (expiresAt > MaxDays)
            throw new DomainException(string.Format(
                IdentityCommonMessages.InvitationExpirationDaysExceeded,
                expiresAt,
                MaxDays)
            );

        return workspaceId != Guid.Empty
            ? new WorkspaceInvitation(workspaceId, inviteeEmail, role, DateTime.UtcNow.AddDays(expiresAt))
            : throw new DomainException(IdentityCommonMessages.WorkspaceIdIsRequired);
    }
}