using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Apartments;

public static class ApartmentErrors
{
    public static Error NotFound = new ("Apartment.Found", "The Apartment with the specified identifier was not found");
    public static Error Overlap = new ("Apartment.Overlap", "The current Apartment is overlapping with an existing one");
    public static Error NotReserved = new ("Apartment.NotReserved", "The Apartment is not pending");
    public static Error NotConfirmed = new ("Apartment.NotConfirmed", "The Apartment is not Confirmed");
    public static Error AlreadyStarted = new ("Apartment.AlreadyStarted", "The Apartment has already started");
}