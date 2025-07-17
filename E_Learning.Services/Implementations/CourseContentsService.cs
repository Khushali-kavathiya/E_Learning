using E_Learning.Domain.Entities;
using E_Learning.Repositories.Interfaces;
using E_Learning.Services.Interfaces;
using E_Learning.Services.Models;
using AutoMapper;

namespace E_Learning.Services.Implementations
{
    /// <summary>
    /// courseContentsService class implements ICourseContentsService interface for course content-related operations.
    /// </summary>
    public class CourseContentsService : ICourseContentsService
    {
        private readonly ICourseContentsRepository _courseContentsRepository;
        private readonly IMapper _mapper;

        public CourseContentsService(ICourseContentsRepository courseContentsRepository, IMapper mapper)
        {
            _courseContentsRepository = courseContentsRepository;
            _mapper = mapper;
        }

        /// <inheritdoc>
        public async Task<CourseContentModel> CreateAsync(CourseContentModel model)
        {
            if (!await _courseContentsRepository.CourseExistsAsync(model.CourseId))
                throw new ArgumentException("Course not found.");

            var entity = _mapper.Map<CourseContent>(model);
            await _courseContentsRepository.AddAsync(entity);
            await _courseContentsRepository.SaveChangesAsync();

            return _mapper.Map<CourseContentModel>(entity);
        }

        /// <inheritdoc>
        public async Task<CourseContentModel> GetCourseContentByIdAsync(Guid id)
        {
            var entity = await _courseContentsRepository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<CourseContentModel>(entity);
        }

        /// <inheritdoc>
        public async Task<List<CourseContentModel>> GetCourseContentsByCourseIdAsync(Guid courseId)
        {
            var entities = await _courseContentsRepository.GetCourseContentsByCourseIdAsync(courseId);
            return entities == null ? null : _mapper.Map<List<CourseContentModel>>(entities);
        }

        /// <inheritdoc>
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _courseContentsRepository.DeleteAsync(id);
        }

        /// <inheritdoc>
        public async Task<CourseContentModel> UpdateCourseContentAsync(Guid id, CourseContentModel model)
        {
            var existing = await _courseContentsRepository.GetByIdAsync(id);
            if (existing == null)
                throw new ArgumentException("Course content not found.");

            _mapper.Map(model, existing);
            await _courseContentsRepository.UpdateAsync(existing);
            return _mapper.Map<CourseContentModel>(existing);
        }

    }
}