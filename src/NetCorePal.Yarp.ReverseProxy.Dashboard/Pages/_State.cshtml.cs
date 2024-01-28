using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Yarp.ReverseProxy;
namespace NetCorePal.Yarp.ReverseProxy.Dashboard.Pages;

public class ProxyStateModel : PageModel
{

    public IProxyStateLookup ProxyStateLookup { get; init; }


    public ProxyStateModel(IProxyStateLookup proxyStateLookup)
    {
        this.ProxyStateLookup = proxyStateLookup;
    }
}

