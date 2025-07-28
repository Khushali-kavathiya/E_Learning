using AutoMapper;
using E_Learning.Repositories.Interfaces;
using E_Learning.Services.Models;
using E_Learning.Domain.Entities;
using E_Learning.Services.Interfaces;

namespace E_Learning.Services.Implementations
{
    /// <summary>
    /// CommentService class implements ICommentService interface for comment-related operations.
    /// </summary>
    public class CommentService(ICommentRepository _commentRepository, IMapper _mapper) : ICommentService
    {
        /// <inheritdoc>
        public async Task<CommentModel> AddCommentAsync(CommentModel model)
        {
            var entity = _mapper.Map<Comment>(model);
            var result = await _commentRepository.AddCommentAsync(entity);
            return _mapper.Map<CommentModel>(result);
        }

        /// <inheritdoc>
        public async Task<IEnumerable<CommentModel>> GetCommentsByCourseIdAsync(Guid courseId)
        {
            var comments = await _commentRepository.GetByCourseIdAsync(courseId);
            return _mapper.Map<IEnumerable<CommentModel>>(comments);
        }

        /// <inheritdoc>
        public async Task<CommentModel> GetByIdAsync(Guid commentId)
        {
            var entity = await _commentRepository.GetByIdAsync(commentId);
            return _mapper.Map<CommentModel>(entity);
        }

        /// <inheritdoc>
        public async Task<CommentModel> UpdateCommentAsync(Guid id, CommentModel model)
        {
            var existing = await _commentRepository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.content = model.content;
            existing.UpdatedAt = DateTime.UtcNow;

            await _commentRepository.UpdateCommentAsync(existing);
            return _mapper.Map<CommentModel>(existing);
        }

        /// <inheritdoc>
        public async Task<bool> DeleteCommentAsync(Guid commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null) return false;
            return await _commentRepository.DeleteCommentAsync(comment);
        }

    }
}