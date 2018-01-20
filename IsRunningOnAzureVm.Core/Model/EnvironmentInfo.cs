using System;
using System.Collections.Generic;
using System.Text;

namespace IsRunningOnAzureVm.Core.Model
{
    public class EnvironmentInfo
    {
        public bool IsRunningOnAzure { get; set; }
        public string PublicIpv4Address { get; set; }
        public string VmSize { get; set; }
        public string VmId { get; set; }
        public string SubscriptionId { get; set; }
    }
}
