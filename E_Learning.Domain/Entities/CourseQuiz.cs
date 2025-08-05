
namespace E_Learning.Domain.Entities
{
    /// <summary>
    /// Course quiz entity.
    /// </summary>
    public class CourseQuiz
    {
        /// <summary>
        /// Unique identifier for the course quiz.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Course id for which this quiz is associated.
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// List of quiz questions.
        /// </summary>
        public List<QuizQuestion> Questions { get; set; } = new();
    }

    /// <summary>
    /// Quiz question entity.
    /// </summary>
    public class QuizQuestion
    {
        /// <summary>
        /// Unique identifier for the quiz question.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Question text.
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// Correct answer for the question.
        /// </summary>
        public string CorrectAnswer { get; set; }

        /// <summary>
        /// Difficulty level of the question (e.g., easy, medium, hard).
        /// </summary>
        public string Difficulty { get; set; }

        /// <summary>
        /// Options for the question.
        /// </summary>
        public List<QuizOption> Options { get; set; }
    }

    /// <summary>
    /// Quiz option entity.
    /// </summary>
    public class QuizOption
    {
        /// <summary>
        /// Unique identifier for the quiz option.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Text of the option.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Is the correct answer for the question?
        /// </summary>
        public bool IsCorrect { get; set; }
    }
}