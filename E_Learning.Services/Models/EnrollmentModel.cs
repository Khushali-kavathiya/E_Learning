using E_Learning.Domain.Entities;
using E_Learning.Extensions.Mappings;

namespace E_Learning.Services.Models
{

    /// <summary>
    /// Represents an enrollment record for a user in a course.
    /// </summary>
    public class EnrollmentModel : IMapFrom<Enrollment>
    {
        /// <summary>
        /// Id of the enrolled user.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Id of the course the user is enrolled in.
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// The date and time the user enrolled in the course.
        /// </summary>
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Whether the user has completed the course or not.
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// The percentage of course progress (0 to 100).
        /// </summary>
        public double Progress { get; set; } = 0;

        /// <summary>
        /// The date and time the user completed the course.
        /// </summary>
        public DateTime? CompletedAt { get; set; }

        /// <summary>
        /// The URL of the certificate the user obtained.
        /// </summary>
        public string? CertificateUrl { get; set; }

    }
}

