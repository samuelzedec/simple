namespace BuildingBlocks.Domain.Abstractions.Events;

/// <summary>
/// Representa a interface base para todos os eventos no sistema.
/// </summary>
/// <remarks>
/// Esta interface define o contrato mínimo que todo evento deve implementar,
/// incluindo identificação única e timestamp de ocorrência.
/// </remarks>
public interface IEvent
{
    /// <summary>
    /// Obtém o identificador único do evento.
    /// </summary>
    /// <value>
    /// GUID que identifica unicamente este evento no sistema.
    /// </value>
    Guid EventId { get; }

    /// <summary>
    /// Obtém a data e hora em que o evento ocorreu.
    /// </summary>
    /// <value>
    /// Timestamp em UTC indicando quando o evento foi criado.
    /// </value>
    DateTime OccurredOn { get; }
}