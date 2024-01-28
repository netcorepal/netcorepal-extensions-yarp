using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddYarpDashboard(this IServiceCollection services)
		{
			services.AddRazorPages();
			return services;
		}


		public static IApplicationBuilder UseYarpDashboardStaticFiles(this IApplicationBuilder builder)
		{
			builder.UseStaticFiles(new StaticFileOptions
			{
				RequestPath = "/_static",
				FileProvider = new EmbeddedFileProvider(typeof(ServiceCollectionExtensions).Assembly, "NetCorePal.Yarp.ReverseProxy.Dashboard.wwwroot")
			});
			return builder;
		}

		public static IApplicationBuilder UseYarpDashboard(this IApplicationBuilder builder, int? port = null)
		{

			if (port == null)
			{
				builder.UseEndpoints(endpoint => endpoint.MapRazorPages());
			}
			else
			{
				builder.UseEndpoints(endpoint => endpoint.MapRazorPages().RequireHost($"*:{port}"));
			}

			return builder;
		}
	}
}

