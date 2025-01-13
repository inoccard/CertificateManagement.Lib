using System;

namespace Sds.CertificateManagement.Models;

/// <summary>
/// 
/// </summary>
public abstract class CertificateRequest
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string EventTitle { get; set; }
    public string Organization { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public int Hours { get; set; } = 8;
}