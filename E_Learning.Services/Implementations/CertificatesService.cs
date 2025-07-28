using E_Learning.Services.Interfaces;
using E_Learning.Services.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class CertificateService : ICertificatesService
{
    /// <summary>
    /// Generates a certificate for a user in a completed course.
    /// </summary>
    public byte[] GenerateCertificate(CertificateModel model)
    {
        // Set the license BEFORE using any QuestPDF features
        QuestPDF.Settings.License = LicenseType.Community;
        
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(50);
                page.Size(PageSizes.A4);
                page.Content().Column(col =>
                {
                    col.Item().Text("Certificate of Completion").FontSize(28).Bold().AlignCenter();
                    col.Item().Text($"This certifies that").AlignCenter();
                    col.Item().Text(model.UserFullName).FontSize(24).Bold().AlignCenter();
                    col.Item().Text($"has successfully completed the course").AlignCenter();
                    col.Item().Text(model.CourseTitle).FontSize(20).Bold().AlignCenter();
                    col.Item().Text($"on {model.CompletedAt:MMMM dd, yyyy}").AlignCenter();
                });
            });
        });

        return document.GeneratePdf();
    }
}
