using NetCorePal.ServiceDiscovery;
using Yarp.ReverseProxy.Configuration;

namespace NetCorePal.Yarp.ServiceDiscovery
{
    public class ServiceDiscoveryProxyConfigProvider : IProxyConfigProvider
    {
        IServiceDiscoveryClient _client;
        public ServiceDiscoveryProxyConfigProvider(IServiceDiscoveryClient serviceDiscoveryClient)
        {
            _client = serviceDiscoveryClient;
        }

        public IProxyConfig GetConfig()
        {

            var newservices = _client.GetServiceClusters().Select(p => new ClusterConfig
            {

                ClusterId = p.Value.ClusterId,
                Metadata = p.Value.Metadata,
                LoadBalancingPolicy = p.Value.LoadBalancingPolicy,
                Destinations = p.Value.Destinations.ToDictionary(d => d.Key, d => new DestinationConfig { Address = d.Value.Address, Metadata = d.Value.Metadata })
            });
            return new ServiceDiscoveryProxyConfig { Clusters = newservices.ToList(), ChangeToken = _client.GetReloadToken() };
        }
    }
}
