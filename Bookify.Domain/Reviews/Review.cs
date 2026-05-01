using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Reviews
{
    public sealed class Review : Entity
    {
        private Review()
        {

        }
        private Review(
                Guid id,
                Guid apartmentId,
                Guid userId,
                Guid bookingId,
                Rating rating,
                Comment comment,
                DateTime createdOnUtc) : base(id)   
        {
            Id = id;
            ApartmentId = apartmentId;
            UserId = userId;
            BookingId = bookingId;
            Rating = rating;
            Comment = comment;
            CreatedOnUtc = createdOnUtc;
        }

        public Guid Id { get; }
        public Guid ApartmentId { get; }
        public Guid UserId { get; }
        public Guid BookingId { get; }
        public Rating Rating { get; }
        public Comment Comment { get; }
        public DateTime CreatedOnUtc { get; }
    }
}
