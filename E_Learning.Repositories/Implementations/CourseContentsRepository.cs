using E_Learning.Repositories.Data;
using E_Learning.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using E_Learning.Domain.Entities;

namespace E_Learning.Repositories.Implementations
{
    /// <inheritdoc />
    public class CourseContentsRepository : ICourseContentsRepository
    {
        private readonly E_LearningDbContext _context;
        public CourseContentsRepository(E_LearningDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task AddAsync(CourseContent content)
           => await _context.CourseContents.AddAsync(content);

        /// <inheritdoc />
        public async Task<bool> CourseExistsAsync(Guid courseId)
            => await _context.Courses.AnyAsync(c => c.Id == courseId);

        /// <inheritdoc />    
        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();

        /// <inheritdoc />
        public async Task<CourseContent> GetByIdAsync(Guid id)
            => await _context.CourseContents.FindAsync(id);

        /// <inheritdoc />
        public async Task<List<CourseContent>> GetCourseContentsByCourseIdAsync(Guid courseId)
            => await _context.CourseContents.Where(c => c.CourseId == courseId).OrderBy(c => c.Order).ToListAsync();

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(Guid id)
        {
            var content = await GetByIdAsync(id);
            if (content == null)
                return false;

            _context.CourseContents.Remove(content);
            await SaveChangesAsync();
            return true;
        }

        /// <inheritdoc />
        public async Task UpdateAsync(CourseContent courseContent)
        {
            _context.CourseContents.Update(courseContent);
            await SaveChangesAsync();
        }
    }
}