using System.Collections.Concurrent;
using System.Security.Cryptography.X509Certificates;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Yarp.Kubernetes.Controller;
using Yarp.Kubernetes.Controller.Caching;
using Yarp.Kubernetes.Controller.Certificates;
using Yarp.Kubernetes.Controller.Client;

namespace NetCorePal.Yarp.Kubernetes.Controller.IngressCertificates;

public class IngressServerCertificateManager : IHostedService
{
    private readonly IngressServerCertificateSelector _certificateSelector;

    private readonly object _lock = new object();

    private readonly ICache _cache;

    private readonly ILogger _logger;

    private readonly ConcurrentDictionary<NamespacedName, X509Certificate2> _secrets = new();

    private readonly ICertificateHelper _certificateHelper;

    public IngressServerCertificateManager(
        IngressServerCertificateSelector certificateSelector,
        ICache cache,
        ICertificateHelper certificateHelper,
        IResourceInformer<V1Secret> secretInformer,
        IResourceInformer<V1Ingress> ingressInformer,
        ILogger<IngressServerCertificateManager> logger)
    {
        _certificateSelector = certificateSelector;
        _cache = cache;
        _certificateHelper = certificateHelper;
        secretInformer.Register(Update);
        ingressInformer.Register(Update);
        _logger = logger;
    }

    private void Update(WatchEventType eventType, V1Ingress ingress)
    {
        try
        {
            ResetCertificates();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Update ingress certificate error when update ingress");
        }
    }

    private void Update(WatchEventType eventType, V1Secret resource)
    {
        try
        {
            NamespacedName n = new NamespacedName(resource.Namespace(), resource.Name());
            switch (eventType)
            {
                case WatchEventType.Added:
                case WatchEventType.Modified:
                    _secrets.TryAdd(n, _certificateHelper.ConvertCertificate(n, resource));
                    break;
                case WatchEventType.Deleted:
                    _secrets.TryRemove(n, out _);
                    break;
            }

            ResetCertificates();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Update ingress certificate error when update secret");
        }
    }


    private void ResetCertificates()
    {
        lock (_lock)
        {
            var certs = new Dictionary<string, X509Certificate2>();
            var wildcardCerts = new Dictionary<string, X509Certificate2>();
            foreach (var ingress in _cache.GetIngresses())
            {
                if (ingress.Spec?.Tls == null) continue;

                foreach (var tls in ingress.Spec.Tls)
                {
                    if (!string.IsNullOrEmpty(tls.SecretName))
                    {
                        var secretName = new NamespacedName(ingress.Metadata.Namespace(), tls.SecretName);
                        if (_secrets.TryGetValue(secretName, out var certificate))
                        {
                            foreach (var host in tls.Hosts)
                            {
                                if (IsWildcardDomain(host))
                                {
                                    wildcardCerts[host] = certificate;
                                }
                                else
                                {
                                    certs[host] = certificate;
                                }
                            }
                        }
                    }
                }
            }

            Interlocked.Exchange(ref _certificateSelector._domainCertificates, certs);
            Interlocked.Exchange(ref _certificateSelector._wildcardDomainCertificates, wildcardCerts);
        }
    }


    bool IsWildcardDomain(string domainName)
    {
        return domainName.StartsWith("*", StringComparison.Ordinal);
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

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}