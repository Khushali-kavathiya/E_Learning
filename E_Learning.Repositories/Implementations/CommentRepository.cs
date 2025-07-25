using E_Learning.Repositories.Data;
using E_Learning.Repositories.Interfaces;
using E_Learning.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Implementations
{
    /// <summary>
    /// Class to interact with Comment table in the database.
    /// </summary>
    /// <param name="context">Database context.</param>
    public class CommentRepository(E_LearningDbContext _context) : ICommentRepository
    {
        /// <inheritdoc />
        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Comment>> GetByCourseIdAsync(Guid courseId)
            => await _context.Comments.Where(c => c.CourseId == courseId)
                                      .Include(c => c.User)
                                      .OrderByDescending(c => c.CreatedAt)
                                      .ToListAsync();


        /// <inheritdoc />
        public async Task<Comment> GetByIdAsync(Guid commentId)
            => await _context.Comments.FindAsync(commentId);

        /// <inheritdoc />
        public async Task UpdateCommentAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<bool> DeleteCommentAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            var result = await _context.SaveChangesAsync();
            return result > 0; // returns true if a record was affected
        }
    }
}