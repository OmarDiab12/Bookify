namespace Bookify.Domain.Abstractions;

public record Error(string Code, string Name)
{
    public static Error None => new(string.Empty, nameof(None));
    public static Error NullValue => new("Error.NullValue", "Null value was provided");
}