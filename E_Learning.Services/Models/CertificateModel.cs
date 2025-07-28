
namespace E_Learning.Services.Models
{
    /// <summary>
    /// Certification model for generating a certificate.
    /// </summary>
    public class CertificateModel
    {
        /// <summary>
        /// User's full name.
        /// </summary>
        public string UserFullName { get; set; }

        /// <summary>
        /// Course title.
        /// </summary>
        public string CourseTitle { get; set; }

        /// <summary>
        /// Course completion date.
        /// </summary>
        public DateTime CompletedAt { get; set; }
    }
}