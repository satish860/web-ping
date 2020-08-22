using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace webping
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration configuration;

        public Worker(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var target = configuration["TARGET"];
            var head = configuration["METHOD"];
            var interval = int.Parse(configuration["INTERVAL"]);
            HttpClient client = new HttpClient();
            while (!stoppingToken.IsCancellationRequested)
            {
                var httpresponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, target));
                Console.WriteLine($"Result of ping {target} is {httpresponse.StatusCode.ToString()}");
                await Task.Delay(interval);
            }
        }
    }
}
