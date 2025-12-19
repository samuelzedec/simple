using System.Text.RegularExpressions;
using BuildingBlocks.Domain.Exceptions;
using BuildingBlocks.Domain.Resources;
using BuildingBlocks.Domain.ValueObjects;

namespace Identity.Domain.ValueObjects;

/// <summary>
/// Representa um objeto de valor imutável para endereços de email.
/// Garante que o email siga um formato específico e respeite restrições de comprimento.
/// </summary>
public sealed partial record Email : BaseValueObject
{
    /// <summary>
    /// Define o padrão de expressão regular usado para validar o formato de endereços de email.
    /// O padrão garante conformidade com uma estrutura básica de email, incluindo uma parte local,
    /// um símbolo "@" e uma parte de domínio com um sufixo válido.
    /// </summary>
    public const string RegexPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

    /// <summary>
    /// Especifica o comprimento máximo permitido para um endereço de email.
    /// Este valor segue as restrições definidas pelo padrão RFC 5321, onde o comprimento máximo
    /// de um endereço de email não pode exceder 254 caracteres.
    /// </summary>
    public const int MaxLength = 254;

    /// <summary>
    /// Obtém o valor do email.
    /// Representa o valor de string subjacente do endereço de email.
    /// O valor deve estar em conformidade com o formato especificado definido em <c>RegexPattern</c>.
    /// </summary>
    public string Value { get; }

    private Email(string value)
        => Value = value;

    /// <summary>
    /// Cria uma nova instância de <see cref="Email"/> com base na string de email fornecida.
    /// Valida a entrada para valores nulos ou espaços em branco, restrições de comprimento e conformidade de formato conforme o padrão regex especificado.
    /// </summary>
    /// <param name="value">A representação em string do endereço de email a ser validado e encapsulado em um objeto <see cref="Email"/>.</param>
    /// <returns>Uma instância de <see cref="Email"/> encapsulando o endereço de email validado.</returns>
    /// <exception cref="DomainException">
    /// Lançada quando a string de email fornecida está vazia ou nula, excede o comprimento máximo permitido,
    /// ou não está em conformidade com o formato exigido.
    /// </exception>
    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(CommonMessages.EmailEmpty);

        value = value.Trim().ToLower();

        if (value.Length > MaxLength)
            throw new DomainException(string.Format(CommonMessages.EmailInvalidLength, MaxLength));

        return ValidateRegex().IsMatch(value)
            ? new Email(value)
            : throw new DomainException(CommonMessages.EmailInvalid);
    }

    [GeneratedRegex(RegexPattern)]
    private static partial Regex ValidateRegex();

    public static implicit operator string(Email email)
        => email.Value;
    
    public static implicit operator Email(string email)
        => Create(email);
}