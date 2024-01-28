using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Yarp.Kubernetes.Controller.Certificates;

namespace NetCorePal.Yarp.Kubernetes.Controller.IngressCertificates;

public static class ReverseProxyBuilderExtensions
{
    public static IReverseProxyBuilder UseIngressServerCertificateSelector(this IReverseProxyBuilder builder)
    {
        builder.Services.TryAddSingleton<IngressServerCertificateSelector>();
        builder.Services.AddHostedService<IngressServerCertificateManager>();
        builder.Services.Replace(ServiceDescriptor
            .Singleton<IServerCertificateSelector>(p => p.GetRequiredService<IngressServerCertificateSelector>()));
        return builder;
    }
}