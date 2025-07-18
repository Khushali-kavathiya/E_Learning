using E_Learning.Domain.Entities;
using E_Learning.Repositories.Data;
using E_Learning.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Implementations
{
    public class EnrollmentsRepository(E_LearningDbContext _context) : IEnrollmentsRepository
    {
        /// <inheritdoc />
        public async Task<Enrollment> CreateEnrollmentAsync(Enrollment enrollment)
        {
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return enrollment;
        }

        /// <inheritdoc />
        public async Task<Enrollment> GetEnrollmentByIdAsync(Guid id)
            => await _context.Enrollments.FindAsync(id);

        /// <inheritdoc />
        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByUserIdAsync(string userId)
            => await _context.Enrollments.Where(e => e.UserId == userId).ToListAsync();

        /// <inheritdoc />
        public async Task UpdateEnrollmentAsync(Enrollment enrollment)
        {
            _context.Enrollments.Update(enrollment);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteEnrollmentAsync(Guid id)
        {
            var enrollment = await GetEnrollmentByIdAsync(id);
            if (enrollment == null)
                return;

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
        }
    }
}