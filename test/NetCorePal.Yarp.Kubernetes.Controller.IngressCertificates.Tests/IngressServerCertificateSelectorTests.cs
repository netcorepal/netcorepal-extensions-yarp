using k8s;
using k8s.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NetCorePal.Yarp.Kubernetes.Controller.IngressCertificates;
using Yarp.Kubernetes.Controller.Caching;
using Yarp.Kubernetes.Controller.Certificates;
using Yarp.Kubernetes.Controller.Client;

namespace NetCorePal.Yarp.Kubernetes.Controller.IngressCertificates.Tests;

public class IngressServerCertificateSelectorTests
{
    [Fact]
    public void Test1()
    {
        var cacheMock = new Mock<ICache>();
        var certificateHelperLoggerMock = new Mock<ILogger<CertificateHelper>>();
        ICertificateHelper certificateHelper = new CertificateHelper(certificateHelperLoggerMock.Object);

        var secretInformer = new Mock<IResourceInformer<V1Secret>>();

        ResourceInformerCallback<V1Secret>? secretCallback = null;
        secretInformer.Setup(p => p.Register(It.IsAny<ResourceInformerCallback<V1Secret>>()))
            .Callback((ResourceInformerCallback<V1Secret> c) => secretCallback = c);

        ResourceInformerCallback<V1Ingress>? ingressCallback = null;
        var ingressInformer = new Mock<IResourceInformer<V1Ingress>>();
        ingressInformer.Setup(p => p.Register(It.IsAny<ResourceInformerCallback<V1Ingress>>()))
            .Callback((ResourceInformerCallback<V1Ingress> c) => ingressCallback = c);

        var loggerMock = new Mock<ILogger<IngressServerCertificateManager>>();
        var manager = new IngressServerCertificateManager(
            new IngressServerCertificateSelector(),
            cacheMock.Object,
            certificateHelper,
            secretInformer.Object,
            ingressInformer.Object,
            loggerMock.Object);

        Assert.NotNull(secretCallback);
        Assert.NotNull(ingressCallback);

        secretCallback(WatchEventType.Added, new V1Secret());
    }
}