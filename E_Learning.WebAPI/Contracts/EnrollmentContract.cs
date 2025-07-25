using E_Learning.Extensions.Attributes;
using E_Learning.Extensions.Mappings;
using E_Learning.Services.Models;

namespace E_Learning.WebAPI.Contracts
{
    /// <summary>
    /// EnrollmentContract class for creating an enrollment.
    /// </summary>
    public class EnrollmentContract : IMapFrom<EnrollmentModel>
    {
        /// <summary>
        /// Id of the enrolled user.
        /// </summary>
        [CreateOnly]
        public string? UserId { get; set; }

        /// <summary>
        /// Id of the course the user is enrolled in.
        /// </summary>
        [CreateOnly]
        public Guid CourseId { get; set; }

        /// <summary>
        /// The date and time the user enrolled in the course.
        /// </summary>
        [CreateOnly]
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