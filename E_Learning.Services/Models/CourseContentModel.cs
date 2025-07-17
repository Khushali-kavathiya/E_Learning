using E_Learning.Domain.Enums;

namespace E_Learning.Services.Models
{
    public class CourseContentModel
    {
         public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string ContentUrl { get; set; }
        public CourseContentType ContentType { get; set; }
        public int Order { get; set; }
    }
}