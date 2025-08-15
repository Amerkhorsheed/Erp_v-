using System.Linq;
using System.Net.NetworkInformation;

namespace Erp_V1.Common
{
    public static class MachineInfo
    {
        /// <summary>
        /// Returns the MAC address of the first operational network interface, or "Unknown MAC" if none is found.
        /// </summary>
        public static string GetPrimaryMacAddress()
        {
            var nic = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(n =>
                    n.OperationalStatus == OperationalStatus.Up &&
                    n.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    n.GetPhysicalAddress().ToString() != string.Empty
                )
                .FirstOrDefault();

            if (nic == null)
                return "Unknown MAC";

            return string.Join(":", nic
                .GetPhysicalAddress()
                .GetAddressBytes()
                .Select(b => b.ToString("X2")));
        }
    }
}
