using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Domain.Exceptions;
using Identity.Domain.Enums;
using Identity.Domain.Resources;

namespace Identity.Domain.Entities;

/// <summary>
/// Representa a associação entre um usuário e um workspace dentro do sistema.
/// </summary>
/// <remarks>
/// Esta entidade define o relacionamento entre um usuário e um workspace, incluindo o papel do usuário,
/// o status de ativação e a data em que a associação foi criada. Ela desempenha um papel central
/// no gerenciamento de workspaces e controle de acesso da aplicação.
/// </remarks>
public sealed class UserWorkspace : BaseEntity
{
    /// <summary>
    /// Obtém o identificador único do usuário associado a este workspace.
    /// </summary>
    /// <remarks>
    /// Esta propriedade estabelece o relacionamento entre um usuário e um workspace dentro do sistema.
    /// O valor deve ser um <see cref="Guid"/> válido e não vazio, sendo definido durante a criação
    /// da instância de <see cref="UserWorkspace"/>.
    /// </remarks>
    public Guid UserId { get; private set; }

    /// <summary>
    /// Obtém o identificador único do workspace associado a este usuário.
    /// </summary>
    /// <remarks>
    /// Esta propriedade representa o workspace ao qual o usuário pertence no sistema.
    /// O valor deve ser um <see cref="Guid"/> válido e não vazio, sendo atribuído durante
    /// a criação da instância de <see cref="UserWorkspace"/>.
    /// </remarks>
    public Guid WorkspaceId { get; private set; }

    /// <summary>
    /// Obtém o papel atribuído ao usuário dentro de um workspace específico.
    /// </summary>
    /// <remarks>
    /// Esta propriedade define o nível de acesso e permissões que o usuário possui
    /// no contexto de um workspace. Os valores válidos estão definidos no enum
    /// <see cref="UserRole"/>. O papel é definido na criação da entidade e pode ser
    /// alterado por meio do método <see cref="ChangeRole(UserRole)"/>.
    /// </remarks>
    public UserRole Role { get; private set; }

    /// <summary>
    /// Indica se o usuário está ativo no contexto deste workspace.
    /// </summary>
    /// <remarks>
    /// Um estado ativo indica que o usuário pode acessar recursos e executar operações
    /// dentro do workspace. O status pode ser alterado por meio dos métodos
    /// <see cref="Activate"/> e <see cref="Deactivate"/>.
    /// </remarks>
    public bool IsActive { get; private set; }

    /// <summary>
    /// Navegação para a entidade de usuário associada.
    /// </summary>
    public User User { get; private set; } = null!;

    /// <summary>
    /// Navegação para a entidade de workspace associada.
    /// </summary>
    public Workspace Workspace { get; private set; } = null!;

    private UserWorkspace() { }

    private UserWorkspace(Guid userId, Guid workspaceId, UserRole role)
    {
        UserId = userId;
        WorkspaceId = workspaceId;
        Role = role;
        IsActive = true;
    }

    /// <summary>
    /// Cria uma nova instância da entidade <see cref="UserWorkspace"/>.
    /// </summary>
    /// <remarks>
    /// Este método cria o vínculo entre um usuário e um workspace, atribuindo um papel específico
    /// ao usuário. A associação é criada como ativa e a data de vínculo é definida automaticamente.
    /// </remarks>
    /// <param name="userId">Identificador do usuário. Não pode ser um <see cref="Guid"/> vazio.</param>
    /// <param name="workspaceId">Identificador do workspace. Não pode ser um <see cref="Guid"/> vazio.</param>
    /// <param name="role">Papel atribuído ao usuário dentro do workspace.</param>
    /// <returns>Uma nova instância de <see cref="UserWorkspace"/>.</returns>
    /// <exception cref="DomainException">
    /// Lançada quando o <paramref name="userId"/> ou <paramref name="workspaceId"/> é inválido.
    /// </exception>
    public static UserWorkspace Create(Guid userId, Guid workspaceId, UserRole role)
    {
        if (userId == Guid.Empty)
            throw new DomainException(IdentityCommonMessages.UserIdIsRequired);

        return workspaceId != Guid.Empty
            ? new UserWorkspace(userId, workspaceId, role)
            : throw new DomainException(IdentityCommonMessages.WorkspaceIdIsRequired);
    }

    /// <summary>
    /// Altera o papel do usuário dentro do workspace.
    /// </summary>
    /// <remarks>
    /// Este método atualiza o papel do usuário no contexto do workspace.
    /// Caso o novo papel seja igual ao papel atual, uma exceção será lançada.
    /// </remarks>
    /// <param name="newRole">Novo papel a ser atribuído ao usuário.</param>
    /// <exception cref="DomainException">
    /// Lançada quando o usuário já possui o papel informado.
    /// </exception>
    public void ChangeRole(UserRole newRole)
    {
        if (Role == newRole)
            throw new DomainException(IdentityCommonMessages.UserAlreadyHasRole);

        Role = newRole;
    }

    /// <summary>
    /// Desativa o usuário no contexto do workspace.
    /// </summary>
    /// <remarks>
    /// Este método marca o usuário como inativo dentro do workspace.
    /// Caso o usuário já esteja inativo, uma exceção será lançada.
    /// </remarks>
    /// <exception cref="DomainException">
    /// Lançada quando o usuário já está inativo no workspace.
    /// </exception>
    public void Deactivate()
    {
        if (!IsActive)
            throw new DomainException(IdentityCommonMessages.UserAlreadyInactiveInWorkspace);

        IsActive = false;
    }

    /// <summary>
    /// Ativa o usuário no contexto do workspace.
    /// </summary>
    /// <remarks>
    /// Este método marca o usuário como ativo dentro do workspace.
    /// Caso o usuário já esteja ativo, uma exceção será lançada.
    /// </remarks>
    /// <exception cref="DomainException">
    /// Lançada quando o usuário já está ativo no workspace.
    /// </exception>
    public void Activate()
    {
        if (IsActive)
            throw new DomainException(IdentityCommonMessages.UserAlreadyActiveInWorkspace);

        IsActive = true;
    }
}