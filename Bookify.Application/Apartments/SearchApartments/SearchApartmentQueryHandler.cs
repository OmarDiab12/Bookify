using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Booking;
using Dapper;

namespace Bookify.Application.Apartments.SearchApartments;

internal sealed class SearchApartmentQueryHandler : IQueryHandler<SearchAparmentsQuery, IReadOnlyList<ApartmentResponse>>
{
    private static readonly int[] ActiveBookingStatuses =
    {
        (int)BookingStatus.Reserved,
        (int)BookingStatus.Confirmed,
        (int)BookingStatus.Completed
    };
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public SearchApartmentQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<ApartmentResponse>>> Handle(SearchAparmentsQuery request, CancellationToken cancellationToken)
    {
        if (request.StartDate > request.EndDate) return new List<ApartmentResponse>();
        
        using var connection = _sqlConnectionFactory.CreateConnection();
        const string sql = @"Select a.id AS Id, a.address_country as Country From Apartments AS a ";
        
        var apartments = await connection.QueryAsync<ApartmentResponse, AddressResponse, ApartmentResponse>(sql, (apartment, address) =>
        {
            apartment.Address = address;
            return apartment;
        },
        new
        {
            request.StartDate, request.EndDate, ActiveBookingStatuses
        },splitOn: "Country");

        return apartments.ToList();

    }
}