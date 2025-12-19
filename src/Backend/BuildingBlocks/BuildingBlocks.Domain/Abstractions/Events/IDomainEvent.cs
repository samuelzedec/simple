using Mediator;

namespace BuildingBlocks.Domain.Abstractions.Events;

/// <summary>
/// Representa um evento de domínio processado in-process via MediatR.
/// </summary>
/// <remarks>
/// Eventos de domínio são usados para comunicação dentro do mesmo bounded context.
/// São processados de forma síncrona durante a transação via MediatR.
/// </remarks>
public interface IDomainEvent : IEvent, INotification;
