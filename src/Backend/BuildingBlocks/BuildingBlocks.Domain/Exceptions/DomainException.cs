namespace BuildingBlocks.Domain.Exceptions;

/// <summary>
/// Representa erros que ocorrem dentro da camada de domínio da aplicação.
/// </summary>
public sealed class DomainException(string message)
    : Exception(message);
