using Bookify.Domain.Booking;

namespace Bookify.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendAsync(Domain.User.Email recipient, string subject, string body);
}