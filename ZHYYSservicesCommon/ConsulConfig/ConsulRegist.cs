using Consul;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZHYYSservicesCommon.ConsulConfig
{
    public class ConsulRegist
    {
        public ConsulRegist(IConfigurationRoot Root)
        {
            this.root = Root;
        }
        /// <summary>
        /// 服务ID用于注销
        /// </summary>
        private  string ServiceID = string.Empty;

        public IConfigurationRoot root { get; }

        /// <summary>
        /// 服务注册
        /// </summary>
        public async  Task RegistConsul()
        {
            using (ConsulClient consul =new ConsulClient(RegistConsulConfig))
            {
                AgentServiceRegistration res = new AgentServiceRegistration()
                {
                    Address = root["IP"].ToString(),
                    Port=Convert.ToInt32(root["Port"].ToString()),
                    Name=root["ServiceName"].ToString(),
                    ID=root["ServiceName"].ToString()+Guid.NewGuid(),
                    Check=new AgentServiceCheck()
                    {
                        HTTP=$"http://{root["IP"].ToString()}:{root["Port"].ToString()}/api/Health",
                        Interval=TimeSpan.FromSeconds(3),
                        DeregisterCriticalServiceAfter=TimeSpan.FromSeconds(5),
                        Timeout=TimeSpan.FromSeconds(3)
                    }

                };
                ServiceID = res.ID;
                await consul.Agent.ServiceRegister(res);
            }
        }
        /// <summary>
        /// 服务注销
        /// </summary>
        /// <param name="lifetime"></param>
        public    void DesregisterConsul(IApplicationLifetime lifetime)
        {
            lifetime.ApplicationStopped.Register( () =>
            {
                ConsulClient consul = new ConsulClient(RegistConsulConfig);

                 consul.Agent.ServiceDeregister(ServiceID);

            });
        }
        /// <summary>
        /// consul集群注册
        /// </summary>
        /// <param name="config"></param>
        public  void RegistConsulConfig(ConsulClientConfiguration config)
        {
            config = new ConsulClientConfiguration()
            {
                Address = new Uri("http://127.0.0.1:8500"),
                Datacenter="dc1"
            };
        }
    }
}
