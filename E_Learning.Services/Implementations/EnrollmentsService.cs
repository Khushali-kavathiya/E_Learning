using E_Learning.Services.Interfaces;
using AutoMapper;
using E_Learning.Domain.Entities;
using E_Learning.Repositories.Interfaces;
using E_Learning.Services.Models;

namespace E_Learning.Services.Implementations
{
    public class EnrollmentsService(IEnrollmentsRepository _enrollmentsRepository, IMapper _mapper) : IEnrollmentsService
    {
        /// <inheritdoc>
        public async Task<EnrollmentModel> CreateEnrollmentAsync(EnrollmentModel model)
        {
            var entity = _mapper.Map<Enrollment>(model);
            var result = await _enrollmentsRepository.CreateEnrollmentAsync(entity);
            return _mapper.Map<EnrollmentModel>(result);
        }

        /// <inheritdoc>
        public async Task<EnrollmentModel> GetEnrollmentByIdAsync(Guid id)
        {
            var entity = await _enrollmentsRepository.GetEnrollmentByIdAsync(id);
            return entity == null ? null : _mapper.Map<EnrollmentModel>(entity);
        }

        /// <inheritdoc>
        public async Task<IEnumerable<EnrollmentModel>> GetByUserIdAsync(string userId)
        {
            var entities = await _enrollmentsRepository.GetEnrollmentsByUserIdAsync(userId);
            return entities == null ? null : _mapper.Map<IEnumerable<EnrollmentModel>>(entities);
        }

        /// <inheritdoc>
        public async Task UpdateEnrollmentAsync(Guid id, EnrollmentModel model)
        {
            var enrollment = await _enrollmentsRepository.GetEnrollmentByIdAsync(id);
            if (enrollment == null)
                throw new InvalidOperationException("Enrollment not found.");
            _mapper.Map(model, enrollment);
            await _enrollmentsRepository.UpdateEnrollmentAsync(enrollment);    
        }

        // /// <inheritdoc>
        // public async Task PatchAsync(Guid id, EnrollmentModel model)
        // {
        //     var enrollment = await _enrollmentsRepository.GetEnrollmentByIdAsync(id);
        //     if (enrollment == null)
        //         throw new InvalidOperationException("Enrollment not found.");
        //     _mapper.Map(model, enrollment);
        //     await _enrollmentsRepository.UpdateEnrollmentAsync(enrollment);
        // }

        /// <inheritdoc>
        public async Task<bool> DeleteEnrollmentAsync(Guid id)
        {
            var enrollment = await _enrollmentsRepository.GetEnrollmentByIdAsync(id);
            if (enrollment == null)
                return false;
            await _enrollmentsRepository.DeleteEnrollmentAsync(id);
            return true;
        }
    }
}