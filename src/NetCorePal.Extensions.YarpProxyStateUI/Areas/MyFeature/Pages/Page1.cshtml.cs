using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Yarp.ReverseProxy;
namespace NetCorePal.Extensions.YarpProxyStateUI.MyFeature.Pages;

public class Page1Model : PageModel
{

    public IProxyStateLookup ProxyStateLookup { get; init; }


    public Page1Model(IProxyStateLookup proxyStateLookup)
    {
        this.ProxyStateLookup = proxyStateLookup;
    }


    public void OnGet()
    {

    }
}

