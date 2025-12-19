using BuildingBlocks.Domain.Abstractions.Events;

namespace BuildingBlocks.Domain.Entities;

/// <summary>
/// Representa a classe base para todas as entidades dentro do domínio.
/// </summary>
public abstract class BaseEntity
    : Tracker, IEquatable<BaseEntity>
{
    private readonly List<IEvent> _events = [];

    /// <summary>
    /// Obtém a coleção somente leitura de todos os eventos.
    /// </summary>
    public IReadOnlyCollection<IEvent> Events => _events.AsReadOnly();

    /// <summary>
    /// Obtém o identificador único da entidade.
    /// </summary>
    public Guid Id { get; } = Guid.CreateVersion7();

    /// <summary>
    /// Adiciona um evento à entidade.
    /// </summary>
    /// <param name="event">
    /// Evento a ser adicionado. Pode implementar <see cref="IDomainEvent"/>,
    /// <see cref="IIntegrationEvent"/>, ou ambos.
    /// </param>
    protected void RaiseEvent(IEvent @event)
        => _events.Add(@event);

    /// <summary>
    /// Remove todos os eventos pendentes.
    /// </summary>
    public void ClearEvents()
        => _events.Clear();

    public bool Equals(BaseEntity? other)
    {
        if (other is null)
            return false;

        if (GetType() != other.GetType())
            return false;

        return ReferenceEquals(this, other) || Id == other.Id;
    }

    public override bool Equals(object? obj)
        => Equals(obj as BaseEntity);

    public override int GetHashCode()
        => Id.GetHashCode();
}