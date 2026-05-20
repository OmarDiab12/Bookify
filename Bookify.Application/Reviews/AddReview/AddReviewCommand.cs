using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Reviews;
namespace Bookify.Application.Reviews.AddReview;

public record AddReviewCommand(Guid BookingId, Rating RatingId, Comment Comment) : ICommand<Guid>;
