namespace BuildingBlocks.Domain.Abstractions.Events;

/// <summary>
/// Representa um evento de integração publicado via mensageria entre bounded contexts.
/// </summary>
/// <remarks>
/// Eventos de integração são usados para comunicação entre módulos/sistemas diferentes.
/// São publicados de forma assíncrona via mensageria.
/// </remarks>
public interface IIntegrationEvent : IEvent;