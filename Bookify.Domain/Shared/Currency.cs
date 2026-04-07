namespace Bookify.Domain.Apartments;

public record Currency
{
    internal static readonly Currency None = new("");
    public static readonly Currency Usd = new("USD");
    public static readonly Currency Eur = new("EUR");

    public string code { get; init; }
    public Currency(string Code) => code = Code;

    public static Currency FromCode(string code)
    {
        return All.FirstOrDefault(x => x.code == code) ?? throw new ApplicationException("Unknown currency code");
    }

    public static readonly IReadOnlyCollection<Currency> All = [ Usd, Eur ];

}