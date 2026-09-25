using System.ComponentModel.DataAnnotations.Schema;

namespace hronaspdotnet.Domain;


    [ComplexType]
    public record Money(
    decimal Amount,
    string Currency
    );

    [ComplexType]
    public record Address(
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country
    );

    [ComplexType]
    public record PersonName(
    string FirstName,
    string LastName,
    string MiddleName,
    string PreferredName
    );

    [ComplexType]
    public record Email(
    string Value
    );

    [ComplexType]
    public record PhoneNumber(
    string CountryCode,
    string Number,
    string Extension
    );

    [ComplexType]
    public record NationalID(
    string Country,
    string IdNumber,
    string Type
    );

    [ComplexType]
    public record Percentage(
    decimal Value
    );

    [ComplexType]
    public record TaxId(
    string Value,
    string Country
    );

    [ComplexType]
    public record LocalTime(
    int Hour,
    int Minute
    );

