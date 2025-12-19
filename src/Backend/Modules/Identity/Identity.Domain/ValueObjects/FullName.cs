using System.Text.RegularExpressions;
using BuildingBlocks.Domain.Exceptions;
using BuildingBlocks.Domain.ValueObjects;
using Identity.Domain.Resources;

namespace Identity.Domain.ValueObjects;

/// <summary>
/// Representa um objeto de valor de nome completo, garantindo validação, padronização e consistência
/// para o formato e estrutura do nome completo de uma pessoa.
/// </summary>
/// <remarks>
/// Este objeto de valor impõe restrições relacionadas ao comprimento mínimo e máximo,
/// caracteres aceitáveis e espaçamento adequado usando expressões regulares.
/// </remarks>
public sealed partial record FullName : BaseValueObject
{
    /// <summary>
    /// Define o padrão utilizado para padronizar espaços em brancos no nome completo.
    /// Esse padrão de expressão regular é usado para identificar e substituir múltiplos espaços por um único espaço,
    /// garantindo consistência no formato do nome.
    /// </summary>
    public const string StandardizationPattern = @"\s+";

    /// <summary>
    /// Define o padrão de expressão regular usado para validar o formato do nome completo.
    /// Ele assegura que o nome obedeça às regras de caracteres permitidos, incluindo suporte a caracteres especiais como
    /// acentos, hífens e apóstrofos nas posições apropriadas.
    /// </summary>
    public const string RegexPattern = @"^[A-Za-zÀ-ÖØ-öø-ÿ]+(?:[ '\-][A-Za-zÀ-ÖØ-öø-ÿ]+)*$";

    /// <summary>
    /// Define o comprimento máximo permitido para o nome completo.
    /// Este valor garante que o nome completo não exceda a restrição superior estabelecida.
    /// </summary>
    public const int MaxLength = 255;

    /// <summary>
    /// Especifica o comprimento mínimo permitido para o nome completo.
    /// Esta constante define um limite inferior restritivo para garantir que o valor atenda às restrições necessárias.
    /// </summary>
    public const int MinLength = 3;

    /// <summary>
    /// Obtém a representação em string do nome completo.
    /// Esta propriedade contém o valor do nome completo e é imutável.
    /// </summary>
    public string Value { get; }

    private FullName(string value)
        => Value = value;

    /// <summary>
    /// Cria uma nova instância do objeto de valor <see cref="FullName"/>.
    /// </summary>
    /// <param name="value">O nome completo a ser validado e armazenado.</param>
    /// <returns>Um novo objeto <see cref="FullName"/> contendo o nome completo validado.</returns>
    /// <exception cref="DomainException">
    /// Lançada quando o nome completo fornecido é nulo, vazio, consiste apenas de espaços em branco,
    /// não corresponde ao formato requerido ou está fora do intervalo de comprimento permitido.
    /// </exception>
    public static FullName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(IdentityCommonMessages.FullNameEmpty);

        value = StandardizationRegex()
            .Replace(value, " ");

        if (!ValidateRegex().IsMatch(value))
            throw new DomainException(IdentityCommonMessages.FullNameInvalid);

        return value.Length is <= MaxLength and >= MinLength
            ? new FullName(value)
            : throw new DomainException(string.Format(
                IdentityCommonMessages.FullNameInvalidLength,
                MinLength,
                MaxLength)
            );
    }

    [GeneratedRegex(StandardizationPattern)]
    private static partial Regex StandardizationRegex();

    [GeneratedRegex(RegexPattern)]
    private static partial Regex ValidateRegex();

    public static implicit operator string(FullName fullName)
        => fullName.Value;

    public static implicit operator FullName(string fullName)
        => Create(fullName);
}