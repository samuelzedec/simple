using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Domain.Exceptions;
using BuildingBlocks.Domain.ValueObjects;
using Identity.Domain.Resources;

namespace Identity.Domain.Entities;

/// <summary>
/// Representa um workspace dentro do sistema, funcionando como raiz do aggregate Workspace.
/// </summary>
/// <remarks>
/// Esta entidade gerencia informações essenciais do workspace, como nome, status de ativação
/// e limite máximo de usuários. Também mantém a relação com os usuários associados
/// através da coleção <see cref="UserWorkspaces"/>.
/// </remarks>
public sealed class Workspace : BaseEntity, IAggregateRoot
{
    /// <summary>
    /// Obtém o nome do workspace.
    /// </summary>
    public Name Name { get; private set; } = null!;

    /// <summary>
    /// Indica se o workspace está ativo.
    /// </summary>
    public bool IsActive { get; private set; }

    /// <summary>
    /// Obtém o número máximo de usuários permitidos neste workspace.
    /// </summary>
    public int MaxUsers { get; private set; }

    /// <summary>
    /// Coleção de associações entre o usuário e os workspaces.
    /// </summary>
    private readonly List<UserWorkspace> _userWorkspaces = [];

    /// <summary>
    /// Obtém uma coleção somente leitura das associações do usuário com workspaces.
    /// </summary>
    public IReadOnlyCollection<UserWorkspace> UserWorkspaces
        => _userWorkspaces.AsReadOnly();

    private Workspace() { }

    private Workspace(Name name, int maxUsers = 10)
    {
        Name = name;
        MaxUsers = maxUsers;
        IsActive = true;
    }

    /// <summary>
    /// Cria uma nova instância de <see cref="Workspace"/> com o nome e limite de usuários informados.
    /// </summary>
    /// <param name="name">Nome do workspace.</param>
    /// <param name="maxUsers">Número máximo de usuários permitidos.</param>
    /// <returns>Uma instância válida de <see cref="Workspace"/>.</returns>
    /// <exception cref="DomainException">
    /// Lançada quando o número máximo de usuários informado é menor ou igual a zero.
    /// </exception>
    public static Workspace Create(string name, int maxUsers = 10)
    {
        return maxUsers > 0
            ? new Workspace(name, maxUsers)
            : throw new DomainException(IdentityCommonMessages.WorkspaceMaxUsersMustBeGreaterThanZero);
    }

    /// <summary>
    /// Verifica se é possível adicionar mais usuários ao workspace.
    /// </summary>
    /// <returns>Retorna <c>true</c> se ainda houver vagas; caso contrário, <c>false</c>.</returns>
    public bool CanAddUser()
        => _userWorkspaces.Count(uw => uw.IsActive) < MaxUsers;

    /// <summary>
    /// Atualiza o nome do workspace.
    /// </summary>
    /// <param name="name">Novo nome do workspace.</param>
    public void UpdateName(string name)
        => Name = name;

    /// <summary>
    /// Desativa o workspace, impedindo operações futuras.
    /// </summary>
    /// <exception cref="DomainException">
    /// Lançada quando o workspace já está inativo.
    /// </exception>
    public void Deactivate()
    {
        if (!IsActive)
            throw new DomainException(IdentityCommonMessages.WorkspaceAlreadyInactive);

        IsActive = false;
    }
}