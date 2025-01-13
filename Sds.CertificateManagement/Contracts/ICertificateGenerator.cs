using Sds.CertificateManagement.Models;

namespace Sds.CertificateManagement.Contracts;

/// <summary>
/// 
/// </summary>
public interface ICertificateGenerator
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public string Generate(CertificateRequest request);
}