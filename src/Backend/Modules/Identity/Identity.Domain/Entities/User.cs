using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Domain.Exceptions;
using Identity.Domain.Resources;
using Identity.Domain.ValueObjects;
using Email = Identity.Domain.ValueObjects.Email;

namespace Identity.Domain.Entities;

/// <summary>
/// Representa um usuário no sistema, atuando como raiz do agregado de usuário.
/// </summary>
/// <remarks>
/// Esta entidade encapsula informações e comportamentos relacionados ao usuário,
/// incluindo nome completo, email, ID do usuário na aplicação e os workspaces aos quais ele pertence.
/// </remarks>
public sealed class User : BaseEntity, IAggregateRoot
{
    /// <summary>
    /// Obtém o nome completo do usuário.
    /// </summary>
    public FullName FullName { get; private set; } = null!;

    /// <summary>
    /// Obtém o email do usuário.
    /// </summary>
    public Email Email { get; private set; } = null!;

    /// <summary>
    /// Obtém o identificador do usuário de autenticação na aplicação.
    /// </summary>
    public Guid ApplicationUserId { get; private set; }

    /// <summary>
    /// Coleção de associações entre o usuário e os workspaces.
    /// </summary>
    private readonly List<UserWorkspace> _userWorkspaces = [];

    /// <summary>
    /// Obtém uma coleção somente leitura das associações do usuário com workspaces.
    /// </summary>
    public IReadOnlyCollection<UserWorkspace> UserWorkspaces
        => _userWorkspaces.AsReadOnly();

    private User() { }

    private User(FullName fullName, Email email, Guid applicationUserId)
    {
        FullName = fullName;
        Email = email;
        ApplicationUserId = applicationUserId;
    }

    /// <summary>
    /// Cria uma nova instância de <see cref="User"/> com as informações fornecidas.
    /// </summary>
    /// <param name="fullName">Nome completo do usuário.</param>
    /// <param name="email">Email do usuário.</param>
    /// <param name="applicationUserId">Identificador do usuário na aplicação.</param>
    /// <returns>Uma instância válida de <see cref="User"/>.</returns>
    /// <exception cref="DomainException">
    /// Lançada quando o <paramref name="applicationUserId"/> é inválido (Guid vazio).
    /// </exception>
    public static User Create(string fullName, string email, Guid applicationUserId)
    {
        return applicationUserId != Guid.Empty
            ? new User(fullName, email, applicationUserId)
            : throw new DomainException(IdentityCommonMessages.AuthenticationIdentifierIsRequired);
    }

    /// <summary>
    /// Atualiza o nome completo do usuário.
    /// </summary>
    /// <param name="fullName">Novo nome completo a ser atribuído.</param>
    /// <exception cref="DomainException">
    /// Lançada quando o valor informado não atende às regras definidas no VO <see cref="FullName"/>.
    /// </exception>
    public void UpdateFullName(string fullName)
        => FullName = fullName;

    /// <summary>
    /// Atualiza o email do usuário.
    /// </summary>
    /// <param name="email">Novo email a ser atribuído.</param>
    /// <exception cref="DomainException">
    /// Lançada quando o valor informado não atende às regras definidas no VO <see cref="Email"/>.
    /// </exception>
    public void UpdateEmail(string email)
        => Email = email;
}