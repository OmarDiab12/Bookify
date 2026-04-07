using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Apartments;

namespace Bookify.Application.Apartments.SearchApartments;

public sealed record SearchAparmentsQuery(DateOnly StartDate, DateOnly EndDate)
    : IQuery<IReadOnlyList<ApartmentResponse>>;