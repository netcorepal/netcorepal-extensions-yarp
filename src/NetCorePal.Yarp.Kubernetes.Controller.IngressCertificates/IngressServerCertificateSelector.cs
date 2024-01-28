using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Connections;
using Yarp.Kubernetes.Controller;
using Yarp.Kubernetes.Controller.Certificates;

namespace NetCorePal.Yarp.Kubernetes.Controller.IngressCertificates;

public class IngressServerCertificateSelector : IServerCertificateSelector
{
    private X509Certificate2? _defaultCertificate = null;

    internal volatile Dictionary<string, X509Certificate2> _domainCertificates = new();

    internal volatile Dictionary<string, X509Certificate2> _wildcardDomainCertificates = new();


    public X509Certificate2? GetCertificate(ConnectionContext connectionContext, string domainName)
    {
        if (_domainCertificates.TryGetValue(domainName, out var certificate))
        {
            return certificate;
        }

        if (TryToWildcardDomain(domainName, out var wildcardDomainName)
            && _wildcardDomainCertificates.TryGetValue(wildcardDomainName, out var certificate1))
        {
            return certificate1;
        }

        return _defaultCertificate;
    }

    public void AddCertificate(NamespacedName certificateName, X509Certificate2 certificate)
    {
        _defaultCertificate = certificate;
    }

    public void RemoveCertificate(NamespacedName certificateName)
    {
        _defaultCertificate = null;
    }

    bool TryToWildcardDomain(string domainName, out string wildcardDomainName)
    {
        wildcardDomainName = string.Empty;
        var index = domainName.IndexOf(".", StringComparison.Ordinal);
        if (index == -1)
        {
            return false;
        }

        wildcardDomainName = string.Concat("*", domainName.AsSpan(index));
        return true;
    }
}