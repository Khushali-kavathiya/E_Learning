using E_Learning.Services.Models;
namespace E_Learning.Services.Interfaces
{
    public interface ICertificatesService
    {
        /// <summary>
        /// Generates a certificate for a user in a completed course.
        /// </summary>
        /// <param name="model">The certificate model containing user and course details.</param>
        /// <returns>The generated certificate as a byte array.</returns>
        byte[] GenerateCertificate(CertificateModel model);
    }
}