using IsRunningOnAzureVm.Core.Model;
using System.Threading.Tasks;

namespace IsRunningOnAzureVm.Core.Infrastructure
{
    public interface IAzureVmGuard
    {
        Task<EnvironmentInfo> GetEnvironmentInfoAsync();
    }
}
