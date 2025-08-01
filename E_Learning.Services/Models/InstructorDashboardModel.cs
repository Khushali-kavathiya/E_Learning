
namespace E_Learning.Services.Models
{
    /// <summary>
    /// Represents the overall dashboard model for an instructor.
    /// </summary>
    public class InstructorDashboardModel
    {
        /// <summary>
        /// Total number of courses taught by the instructor.
        /// </summary>
        public int TotalCourses { get; set; }

        /// <summary>
        /// Total number of enrollments made by the instructor.
        /// </summary>
        public int TotalEnrollments { get; set; }

        /// <summary>
        /// Total earnings made by the instructor from the courses.
        /// </summary>
        public decimal TotalEarnings { get; set; }

        /// <summary>
        /// List of course details in the instructor dashboard.
        /// </summary>
        public List<InstructorDashboardCourseModel> Courses { get; set; }
    }


    /// <summary>
    /// Represents each course's details in the instructor dashboard.
    /// </summary>
    public class InstructorDashboardCourseModel
    {
        /// <summary>
        /// Course unique identifier.
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Title of the course.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Enrollment count for the course.
        /// </summary>
        public int EnrollmentCount { get; set; }

        /// <summary>
        /// Average rating for the course.
        /// </summary>
        public double AverageRating { get; set; }

        /// <summary>
        /// Completion count for the course.
        /// </summary>
        public int CompletionCount { get; set; }

        /// <summary>
        /// Earnings made from the course.
        /// </summary>
        public decimal Earnings { get; set; }
    }
}