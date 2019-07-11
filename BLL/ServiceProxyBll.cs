namespace BLL
{
    using System;
    using System.Configuration;
    using System.ServiceModel;
    using System.Collections.Generic;
    using System.Linq;
    using BLL;
    using System.Xml;
   
    public static class ServiceProxyBll
    {
        /// <summary>
        /// 客户端终节点
        /// </summary>
        public static string Endpoint
        {
            get;
            set;
        }

        /// <summary>
        /// 创建客户端服务
        /// </summary>
        public static HSSEService.HSSEServiceClient CreateServiceClient()
        {                      
            var proxy = new HSSEService.HSSEServiceClient();
            ConfigEndpointAddress<HSSEService.HSSEService>(proxy, Endpoint);
            return proxy;
        }

        /// <summary>
        /// 根据web.config中服务器端主机地址配置服务通道的终端地址。
        /// </summary>
        /// <typeparam name="TChannel">通道类型</typeparam>
        /// <param name="proxy">代理</param>
        /// <param name="endpointAddress">服务访问地址</param>
        public static void ConfigEndpointAddress<TChannel>(System.ServiceModel.ClientBase<TChannel> proxy, string endpointAddress)
            where TChannel : class
        {
            Uri endpointUri = new Uri(endpointAddress);            
            EndpointAddress endPointAddress = new EndpointAddress(endpointUri);            
            proxy.Endpoint.Address = endPointAddress;            
        }       
    }
}
