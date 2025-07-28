using E_Learning.Domain.Entities;
using E_Learning.Repositories.Data;
using E_Learning.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Repositories.Implementations
{
    public class EnrollmentsRepository(E_LearningDbContext _context, IHttpContextAccessor _httpContextAccessor) : IEnrollmentsRepository
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

        /// <inheritdoc />
        public async Task<Enrollment> GetByCourseAndUserAsync(Guid courseId, string userId)
            => await _context.Enrollments.FirstOrDefaultAsync(e => e.CourseId == courseId && e.UserId == userId);

        /// <inheritdoc />
        public async Task<bool> MarkAsCompletedAsync(Guid courseId, string userId)
        {
            var enrollment = await GetByCourseAndUserAsync(courseId, userId);
            if (enrollment == null || enrollment.IsCompleted) return false;

            enrollment.IsCompleted = true;
            enrollment.CompletedAt = DateTime.UtcNow;

            // Generate absolute certification URL and save to database
            var request = _httpContextAccessor.HttpContext?.Request;
            var baseUrl = $"{request?.Scheme}://{request?.Host}";
            enrollment.CertificateUrl = $"{baseUrl}/enrollments/{enrollment.Id}/certificate";
            await _context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc />
        public async Task<Enrollment?> GetWithCourseAndUserAsync(Guid enrollmentId, string userId)
        {
            return await _context.Enrollments.Include(e => e.Course)
                                             .Include(e => e.User)
                                             .FirstOrDefaultAsync(e => e.Id == enrollmentId && e.UserId == userId && e.IsCompleted);
        }
    }
}