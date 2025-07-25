using E_Learning.Repositories.Interfaces;
using E_Learning.Domain.Entities;
using E_Learning.Repositories.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Implementations
{
    /// <summary>
    /// Class to interact with CourseRating table in the database.
    /// </summary>
    public class CourseRatingsRepository(E_LearningDbContext _context) : ICourseRatingsRepository
    {
        /// <inheritdoc />
        public async Task<CourseRating> AddOrUpdateRatingAsync(CourseRating rating)
        {
            var existing = await _context.CourseRatings.FirstOrDefaultAsync(r => r.CourseId == rating.CourseId && r.UserId == rating.UserId);
            if (existing != null)
            {
                existing.Rating = rating.Rating;
                existing.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                rating.Id = Guid.NewGuid();
                _context.CourseRatings.Add(rating);
            }

            await _context.SaveChangesAsync();
            return existing ?? rating;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CourseRating>> GetAllByCourseAsync(Guid courseId)
            => await _context.CourseRatings.Where(r => r.CourseId == courseId).ToListAsync();

        /// <inheritdoc />
        public async Task<CourseRating> GetByIdAsync(Guid ratingId)
            => await _context.CourseRatings.FindAsync(ratingId);

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(CourseRating rating)
        {
            _context.CourseRatings.Remove(rating);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}