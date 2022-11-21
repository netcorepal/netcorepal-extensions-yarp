using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using NetCorePal.Extensions.YarpProxyStateUI;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Builder;
namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddYarpProxyStateUI(this IServiceCollection services)
		{
			services.AddRazorPages();
			return services;
		}
		public static void UseYarpProxyStateUI(this IApplicationBuilder builder, int? port = null)
		{
			builder.UseStaticFiles(new StaticFileOptions
			{
				RequestPath = "/_static",
				FileProvider = new EmbeddedFileProvider(typeof(ServiceCollectionExtensions).Assembly, "NetCorePal.Extensions.YarpProxyStateUI.wwwroot")
			});
			if (port == null)
			{
				builder.UseEndpoints(endpoint => endpoint.MapRazorPages());
			}
			else
			{
				builder.UseEndpoints(endpoint => endpoint.MapRazorPages().RequireHost($"*:{port}"));
			}
		}
	}
}

