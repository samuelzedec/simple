using System.Text.RegularExpressions;
using BuildingBlocks.Domain.Exceptions;

namespace BuildingBlocks.Domain.ValueObjects;

/// <summary>
/// Representa um Value Object de nome genérico utilizado no domínio.
/// </summary>
/// <remarks>
/// Este Value Object encapsula regras comuns para nomes exibidos ao usuário,
/// como normalização de espaços, validação de tamanho mínimo e máximo
/// e garantia de imutabilidade. Ele pode ser reutilizado em diferentes
/// contextos do domínio, como produtos, equipes (tenants) ou outras entidades.
/// </remarks>
public sealed partial record Name : BaseValueObject
{
    /// <summary>
    /// Padrão de expressão regular utilizado para normalização de espaços em branco.
    /// </summary>
    /// <remarks>
    /// Este padrão identifica uma ou mais ocorrências consecutivas de espaços
    /// para que sejam substituídas por um único espaço.
    /// </remarks>
    public const string RegexPattern = @"\s+";

    /// <summary>
    /// Define o tamanho máximo permitido para o nome.
    /// </summary>
    public const int MaxLength = 155;

    /// <summary>
    /// Define o tamanho mínimo permitido para o nome.
    /// </summary>
    public const int MinLength = 2;

    /// <summary>
    /// Obtém o valor textual do nome.
    /// </summary>
    public string Value { get; }

    private Name(string value)
        => Value = value;

    /// <summary>
    /// Cria uma nova instância do Value Object <see cref="Name"/>.
    /// </summary>
    /// <remarks>
    /// Este método normaliza os espaços em branco, valida se o valor informado
    /// não é nulo ou vazio e verifica se o tamanho do nome está dentro
    /// dos limites permitidos.
    /// </remarks>
    /// <param name="value">Texto que representa o nome.</param>
    /// <returns>Uma instância válida de <see cref="Name"/>.</returns>
    /// <exception cref="DomainException">
    /// Lançada quando o valor informado é inválido ou não atende às regras definidas.
    /// </exception>
    public static Name Create(string value)
    {
        value = StandardizationRegex()
            .Replace(value, " ");

        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("");

        return value.Length is <= MaxLength and >= MinLength
            ? new Name(value)
            : throw new DomainException("");
    }

    [GeneratedRegex(RegexPattern)]
    private static partial Regex StandardizationRegex();

    public static implicit operator string(Name name)
        => name.Value;

    public static implicit operator Name(string name)
        => Create(name);
}