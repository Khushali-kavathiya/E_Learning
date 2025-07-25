using AutoMapper;
using E_Learning.Domain.Entities;
using E_Learning.Repositories.Interfaces;
using E_Learning.Services.Models;
using E_Learning.Services.Interfaces;

namespace E_Learning.Services.Implementations
{
    /// <summary>
    /// CourseRatingsService class implements ICourseRatingsService interface for course ratings-related operations.
    /// </summary>
    public class CourseRatingsService(ICourseRatingsRepository _courseRatingsRepository, IMapper _mapper) : ICourseRatingsService
    {
        /// <inheritdoc>
        public async Task<CourseRatingModel> AddOrUpdateRatingAsync(CourseRatingModel model)
        {
            model.UpdatedAt = DateTime.UtcNow;
            var entity = _mapper.Map<CourseRating>(model);
            var result = await _courseRatingsRepository.AddOrUpdateRatingAsync(entity);
            return _mapper.Map<CourseRatingModel>(result);
        }

        /// <inheritdoc>
        public async Task<IEnumerable<CourseRatingModel>> GetRatingsByCourseAsync(Guid courseId)
        {
            var list = await _courseRatingsRepository.GetAllByCourseAsync(courseId);
            return list.Select(_mapper.Map<CourseRatingModel>);
        }

        /// <inheritdoc>
        public async Task<bool> DeleteRatingAsync(Guid ratingId)
        {
            var rating = await _courseRatingsRepository.GetByIdAsync(ratingId);
            if (rating == null) return false;
            return await _courseRatingsRepository.DeleteAsync(rating);
        }
    }
}