using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IsRunningOnAzureVm.Core.Model;
using Newtonsoft.Json;

namespace IsRunningOnAzureVm.Core.Infrastructure
{
    public class AzureVmGuard : IAzureVmGuard
    {
        private readonly static HttpClient _httpClient = new HttpClient();

        static AzureVmGuard()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(@"http://169.254.169.254/metadata/instance?api-version=2017-08-01")                
            };            
            _httpClient.DefaultRequestHeaders.Add("Metadata", "true");
        }

        public async Task<EnvironmentInfo> GetEnvironmentInfoAsync()
        {
            CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

            try
            {
                var req = new HttpRequestMessage(HttpMethod.Get, "");
                var resp = await _httpClient.SendAsync(req, cts.Token);
                resp.EnsureSuccessStatusCode();

                var respBody = await resp.Content.ReadAsStringAsync();
                dynamic json = JsonConvert.DeserializeObject(respBody);

                return new EnvironmentInfo
                {
                    IsRunningOnAzure = true,
                    VmId = json.compute.vmId,
                    PublicIpv4Address = json.network.@interface[0].ipv4.ipAddress[0].publicIpAddress,
                    SubscriptionId = json.compute.subscriptionId,
                    VmSize = json.compute.vmSize
                };
            }
            catch (HttpRequestException)
            {
                return new EnvironmentInfo
                {
                    IsRunningOnAzure = false
                };
            }
            catch (OperationCanceledException) when (cts.IsCancellationRequested)
            {
                // TODO: Handle Azure Instance Metadata request timeout
                throw;
            }
        }
    }
}
