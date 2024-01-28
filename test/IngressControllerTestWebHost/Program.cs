using NetCorePal.Yarp.Kubernetes.Controller.IngressCertificates;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddYarpDashboard();
builder.Services.AddKubernetesReverseProxy(builder.Configuration)
    .UseIngressServerCertificateSelector()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

if (builder.Environment.IsDevelopment())
{
    builder.WebHost.UseKubernetesReverseProxyCertificateSelector();
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("apiPolicy", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();
app.UseYarpDashboardStaticFiles();
app.UseRouting();
app.UseYarpDashboard();
app.MapGet("/", () => "Hello World!");
app.MapReverseProxy();
app.Run();