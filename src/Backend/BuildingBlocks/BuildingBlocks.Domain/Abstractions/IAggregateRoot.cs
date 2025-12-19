namespace BuildingBlocks.Domain.Abstractions;

/// <summary>
/// Interface marcadora para designar uma entidade como raiz de agregação no padrão Domain-Driven Design (DDD).
/// Uma raiz de agregação é o ponto de entrada para acessar o agregado e impor suas invariantes.
/// </summary>
public interface IAggregateRoot;