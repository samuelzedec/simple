namespace BuildingBlocks.Domain.Entities;

/// <summary>
/// Classe base abstrata para rastreamento de auditoria de entidades.
/// </summary>
/// <remarks>
/// Fornece timestamps automáticos de criação, atualização e exclusão lógica (soft delete).
/// </remarks>
public abstract class Tracker
{
    /// <summary>
    /// Obtém a data e hora (UTC) de criação da entidade.
    /// </summary>
    /// <value>Definido automaticamente na instanciação.</value>
    public DateTime CreatedAt { get; } = DateTime.UtcNow;

    /// <summary>
    /// Obtém a data e hora (UTC) da última atualização.
    /// </summary>
    /// <value><c>null</c> se nunca foi atualizada.</value>
    public DateTime? UpdatedAt { get; private set; }

    /// <summary>
    /// Obtém a data e hora (UTC) da exclusão lógica (soft delete).
    /// </summary>
    /// <value><c>null</c> se não foi excluída.</value>
    public DateTime? DeletedAt { get; private set; }

    /// <summary>
    /// Atualiza o timestamp de modificação para o momento atual.
    /// </summary>
    public void UpdateEntity()
        => UpdatedAt = DateTime.UtcNow;

    /// <summary>
    /// Marca a entidade como excluída logicamente (soft delete).
    /// </summary>
    public void DeleteEntity()
        => DeletedAt = DateTime.UtcNow;
}