namespace Identity.Domain.Enums;

/// <summary>
/// Representa os papéis que podem ser atribuídos a um usuário no sistema.
/// </summary>
/// <remarks>
/// Cada papel define um conjunto específico de permissões e responsabilidades
/// dentro do sistema. Esses papéis são utilizados para controle de acesso e para
/// determinar quais operações um usuário pode executar.
/// </remarks>
public enum UserRole
{
    /// <summary>
    /// Define o papel de Administrador no sistema.
    /// </summary>
    /// <remarks>
    /// O papel de Administrador concede ao usuário o mais alto nível de acesso e
    /// permissões dentro do sistema. Usuários com esse papel podem realizar tarefas
    /// administrativas, gerenciar configurações e administrar outros usuários
    /// e seus respectivos papéis.
    /// </remarks>
    Admin = 1,

    /// <summary>
    /// Define o papel de Membro no sistema.
    /// </summary>
    /// <remarks>
    /// O papel de Membro representa um usuário padrão, com acesso às funcionalidades
    /// gerais do sistema. Usuários com esse papel possuem permissões limitadas quando
    /// comparados a usuários com papel administrativo.
    /// </remarks>
    Member = 2
}