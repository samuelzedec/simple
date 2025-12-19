namespace Identity.Domain.Enums;

/// <summary>
/// Representa os possíveis estados de um convite para um workspace.
/// </summary>
/// <remarks>
/// Cada status indica a situação atual do convite e controla o fluxo de aceitação,
/// rejeição ou expiração do mesmo.
/// </remarks>
public enum InvitationStatus
{
    /// <summary>
    /// O convite foi enviado, mas ainda não foi aceito ou rejeitado.
    /// </summary>
    Pending = 1,

    /// <summary>
    /// O convite foi aceito pelo usuário convidado.
    /// </summary>
    Accepted = 2,

    /// <summary>
    /// O convite foi rejeitado pelo usuário convidado.
    /// </summary>
    Rejected = 3,

    /// <summary>
    /// O convite expirou e não pode mais ser aceito.
    /// </summary>
    Expired = 4
}