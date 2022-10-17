using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Yarp.ReverseProxy;
namespace NetCorePal.Extensions.YarpProxyStateUI.Pages;

public class ProxyStateModel : PageModel
{

    public IProxyStateLookup ProxyStateLookup { get; init; }


    public ProxyStateModel(IProxyStateLookup proxyStateLookup)
    {
        this.ProxyStateLookup = proxyStateLookup;
    }


    public void OnGet()
    {

    }
}

