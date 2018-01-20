using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsRunningOnAzureVm.Core.Infrastructure;
using IsRunningOnAzureVm.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace IsRunningOnAzureVm.Web.Controllers
{
    [Route("api/[controller]")]
    public class EnvironmentController : Controller
    {
        private readonly IAzureVmGuard _azureVmGuard;

        public EnvironmentController(
            IAzureVmGuard azureVmGuard
        )
        {
            _azureVmGuard = azureVmGuard;
        }

        // GET api/environment
        [HttpGet]
        public async Task<EnvironmentInfo> Get()
        {
            return await _azureVmGuard.GetEnvironmentInfoAsync();
        }
    }
}
