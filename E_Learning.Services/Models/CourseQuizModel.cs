
using E_Learning.Domain.Entities;

namespace E_Learning.Services.Models
{
    /// <summary>
    /// CourseQuizModel class for creating a course quiz.
    /// </summary>
    public class CourseQuizModel
    {
        /// <summary>
        /// Course id for which the quiz is created.
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Course content text for the quiz.
        /// </summary>
        public string CourseContentText { get; set; }

        public string Difficulty { get; set; }
        public Guid? QuizId { get; set; }
        public List<QuizQuestionModel>? Questions { get; set; }
    }

    /// <summary>
    /// QuizQuestionModel class for creating a quiz question.
    /// </summary>
    public class QuizQuestionModel
    {
        /// <summary>
        /// Question id for the quiz question.
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Question text for the quiz question.
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// Difficulty level for the quiz question(e.g., easy, medium, hard).
        /// </summary>
        public string? Difficulty { get; set; }

        /// <summary>
        /// Options for the quiz question.
        /// </summary>
        public List<QuizOptionModel>? Options { get; set; }
    }

    /// <summary>
    /// QuizOptionModel class for creating a quiz option.
    /// </summary>
    public class QuizOptionModel
    {
        /// <summary>
        /// Option id for the quiz option.
        /// </summary>
        public Guid OptionId { get; set; }

        /// <summary>
        /// Text for the quiz option.
        /// </summary>
        public string Text { get; set; }
    }
}