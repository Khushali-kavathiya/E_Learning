
namespace E_Learning.Services.Models
{
    /// <summary>
    /// Student's dashboard model.
    /// </summary>
    public class StudentDashboardModel
    {
        /// <summary>
        /// Total number of courses enrolled by the student.
        /// </summary>
        public int TotalCoursesEnrolled { get; set; }

        /// <summary>
        /// Total number of courses completed by the student.
        /// </summary>
        public int TotalCoursesCompleted { get; set; }

        /// <summary>
        /// List of course details in the student dashboard.
        /// </summary>
        public List<StudentDashboardCourseModel> Courses { get; set; }
    }

    /// <summary>
    /// Student's course details in the dashboard.
    /// </summary>
    public class StudentDashboardCourseModel
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
        /// Instructor's name for the course.
        /// </summary>
        public string InstructorName { get; set; }

        /// <summary>
        /// Rating given by the student for the course.
        /// </summary>
        public int? RatingGiven { get; set; }

        /// <summary>
        /// Is the course completed by the student?
        /// </summary>
        public bool IsCompleted { get; set; }
    }
}