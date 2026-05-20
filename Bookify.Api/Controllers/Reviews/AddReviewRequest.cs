using Bookify.Domain.Reviews;

namespace Bookify.Api.Controllers.Reviews
{
    public sealed record AddReviewRequest(Guid BookingId, Rating RatingId, Comment Comment);
}