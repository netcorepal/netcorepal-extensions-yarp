# netcorepal-extensions-yarp

[![Release Build](https://img.shields.io/github/actions/workflow/status/netcorepal/netcorepal-extensions-yarp/release.yml?label=release%20build)](https://github.com/netcorepal/netcorepal-extensions-yarp/actions/workflows/release.yml)
[![Preview Build](https://img.shields.io/github/actions/workflow/status/netcorepal/netcorepal-extensions-yarp/dotnet.yml?label=preview%20build)](https://github.com/netcorepal/netcorepal-extensions-yarp/actions/workflows/dotnet.yml)
[![NuGet](https://img.shields.io/nuget/v/NetCorePal.Yarp.ReverseProxy.Dashboard.svg)](https://www.nuget.org/packages/NetCorePal.Yarp.ReverseProxy.Dashboard)
[![MyGet Preview](https://img.shields.io/myget/netcorepal/vpre/NetCorePal.Yarp.ReverseProxy.Dashboard?label=preview)](https://www.myget.org/feed/netcorepal/package/nuget/NetCorePal.Yarp.ReverseProxy.Dashboard)
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/netcorepal/netcorepal-extensions-yarp/blob/main/LICENSE)

The Extensions for [YARP](https://github.com/microsoft/reverse-proxy)

## Package List

|Package|Release|Preview|
|---|---|---|
|NetCorePal.Yarp.ReverseProxy.Dashboard|[![NuGet](https://img.shields.io/nuget/v/NetCorePal.Yarp.ReverseProxy.Dashboard.svg)](https://www.nuget.org/packages/NetCorePal.Yarp.ReverseProxy.Dashboard)|[![MyGet Preview](https://img.shields.io/myget/netcorepal/vpre/NetCorePal.Yarp.ReverseProxy.Dashboard?label=preview)](https://www.myget.org/feed/netcorepal/package/nuget/NetCorePal.Yarp.ReverseProxy.Dashboard)|
|NetCorePal.Yarp.Kubernetes.Controller.IngressCertificates|[![NuGet](https://img.shields.io/nuget/v/NetCorePal.Yarp.Kubernetes.Controller.IngressCertificates.svg)](https://www.nuget.org/packages/NetCorePal.Yarp.Kubernetes.Controller.IngressCertificates)|[![MyGet Preview](https://img.shields.io/myget/netcorepal/vpre/NetCorePal.Yarp.Kubernetes.Controller.IngressCertificates?label=preview)](https://www.myget.org/feed/netcorepal/package/nuget/NetCorePal.Yarp.Kubernetes.Controller.IngressCertificates)|

## Preview Build

Preview NuGet Feed:

```text
https://www.myget.org/F/netcorepal/api/v3/index.json
```

NuGet.config:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="netcorepal" value="https://www.myget.org/F/netcorepal/api/v3/index.json" />
  </packageSources>
</configuration>
```
