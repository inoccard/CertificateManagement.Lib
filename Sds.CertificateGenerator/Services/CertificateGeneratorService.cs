using System;
using System.IO;
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Sds.CertificateGenerator.Contracts;
using Sds.CertificateGenerator.Models;

namespace Sds.CertificateGenerator.Services;

/// <summary>
/// 
/// </summary>
public class CertificateGeneratorService : ICertificateGenerator
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CertificateGeneratorService"/> class.
    /// </summary>
    /// <param name="licenseType">The license type for QuestPDF.</param>
    public CertificateGeneratorService(LicenseType licenseType)
    {
        Settings.License = licenseType;
    }

    /// <summary>
    /// Generate Certificate
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public string Generate(CertificateRequest request)
    {
        try
        {
            var document = CreateCertificateDocument(request);

            var filePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.pdf");

            document.GeneratePdf(filePath);

            return filePath;
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"File not found: {ex.Message}");
            throw;
        }
        catch (IOException ex)
        {
            Console.WriteLine($"IO Exception: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw;
        }
    }

    #region Private Methods

    private static IDocument CreateCertificateDocument(CertificateRequest request)
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                SetBasicConfiguration(page);

                page.Content().Column(column =>
                {
                    SetLogo(column);

                    column.Item().PaddingVertical(10).AlignCenter().Text("CERTIFICADO")
                        .FontSize(28).Bold().FontColor(Colors.Black);

                    SetMainContent(request, column);

                    SetSignatureSpace(column);
                });
            });
        });
    }

    private static void SetBasicConfiguration(PageDescriptor page)
    {
        page.Size(PageSizes.A4.Landscape());
        page.Margin(2, Unit.Centimetre);
        page.PageColor(Colors.White);
        page.DefaultTextStyle(x => x.FontSize(14).FontColor(Colors.Black));
    }

    private static void SetLogo(ColumnDescriptor column)
    {
        var imagePath = Path.Combine(AppContext.BaseDirectory, "img", "OIG.jpeg");
        if (!File.Exists(imagePath))
            throw new FileNotFoundException($"Image not found at {imagePath}");

        column.Item().Height(100).AlignCenter().Row(row =>
        {
            row.RelativeItem().Height(90).Width(90).Image(imagePath, ImageScaling.FitWidth);
        });

        column.Item().PaddingBottom(10);
    }

    private static void SetMainContent(CertificateRequest request, ColumnDescriptor column)
    {
        column.Item().PaddingVertical(10).AlignCenter().Text(txt =>
        {
            txt.Span("Certificamos que ").FontSize(16);
            txt.Span(request.Name).Bold().FontSize(18);
            txt.Span(" participou do evento ").FontSize(16);
            txt.Span(request.EventTitle).Bold().FontSize(18);
            txt.Span(", promovido por ").FontSize(16);
            txt.Span(request.Organization).Bold().FontSize(16);
            txt.Span(", realizado no período de ").FontSize(16);
            txt.Span(request.Date.ToString("dd/MM/yyyy")).Bold().FontSize(16);
            txt.Span(", com carga horária de ").FontSize(16);
            txt.Span($"{request.Hours} horas").Bold().FontSize(16);
            txt.Span(".");
        });
    }

    private static void SetSignatureSpace(ColumnDescriptor column)
    {
        column.Item().PaddingTop(50).Row(row =>
        {
            AddSignature(row, "Prof. Ma. Telma", "Coordenadora do Núcleo");
            AddSignature(row, "Prof. Me. Julio", "Diretor da Unidade de Ciências");
            AddSignature(row, "Prof. Dra. Venancia", "Pró-Reitora de Pós-Graduação, Pesquisa e Extensão");
        });
    }

    private static void AddSignature(RowDescriptor row, string name, string title)
    {
        row.RelativeItem().Column(col =>
        {
            col.Item().AlignCenter().Text(name).Bold();
            col.Item().AlignCenter().Text(title);
        });
    }

    #endregion
}