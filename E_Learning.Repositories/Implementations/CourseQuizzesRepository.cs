
using E_Learning.Repositories.Data;
using E_Learning.Repositories.Interfaces;
using E_Learning.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Implementations
{
    /// <summary>
    /// CourseQuizzesRepository class to interact with CourseQuizzes table in the database.
    /// </summary>
    public class CourseQuizzesRepository(E_LearningDbContext _context) : ICourseQuizzesRepository
    {
        /// <inheritdoc />
        public async Task SaveAsync(CourseQuiz quiz)
        {
            _context.CourseQuizzes.Add(quiz);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<CourseQuiz?> GetByCourseIdAsync(Guid courseId)
        {
            return await _context.CourseQuizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(q => q.CourseId == courseId);
        }
    }
}