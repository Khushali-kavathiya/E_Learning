
using E_Learning.Domain.Entities;
using E_Learning.Extensions.Mappings;

namespace E_Learning.Services.Models
{
    public class CourseRatingModel : IMapFrom<CourseRating>
    {
        /// <summary>
        /// Course id for which the rating was made.
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// User id of the rating giver.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Rating given by the user(e.g., 1-5, 1-star, 2-star, etc. 0 for no rating).
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Review given by the user(e.g., "Great course!", "Not bad" etc. 0 for no review).
        /// </summary>
        public string? Review { get; set; }

        /// <summary>
        /// Creation date and time of the rating.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Update date and time of the rating.
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}