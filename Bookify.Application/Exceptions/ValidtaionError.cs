namespace Bookify.Application.Exceptions;

public sealed record ValidationError(string PropetyName, string ErrorMessage);