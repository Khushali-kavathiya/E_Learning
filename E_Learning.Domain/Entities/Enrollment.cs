using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Learning.Domain.Entities
{
    /// <summary>
    /// Represents an enrollment record for a user in a course.
    /// </summary>
    public class Enrollment
    {
        /// <summary>
        /// Primary key for the enrollment record.
        /// </summary>
        public Guid Id { get; set; }

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

        /// <summary>
        /// Navigation property to the course the user is enrolled in.
        /// </summary>
        public Course Course { get; set; }

        /// <summary>
        /// Navigation property to the user who is enrolled in the course.
        /// </summary>
        public ApplicationUser User { get; set; }
    }
}