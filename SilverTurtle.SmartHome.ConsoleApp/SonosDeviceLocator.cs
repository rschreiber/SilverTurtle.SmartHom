using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SilverTurtle.SmartHome.ConsoleApp
{
    public class SonosDeviceLocator
    {
        private readonly SsdpRadar.FinderService finderService;
        public SonosDeviceLocator()
        {
            finderService = new SsdpRadar.FinderService(2, new TimeSpan(0, 0, 1), new TimeSpan(0, 0, 5));
        }

        public async Task<IEnumerable<SonosDevice>> FindDevicesAsync()
        {
            List<SonosDevice> sonosDevices = new();
            var devices = await finderService.FindDevicesAsync();
            foreach (var device in devices)
            {
                if (IsSonosDevice(device))
                {
                    sonosDevices.Add(new SonosDevice(device));
                }
            }
            return sonosDevices;
        }

        private bool IsSonosDevice(SsdpRadar.SsdpDevice device)
        {
            TcpClient client = new();
            try
            {
                //definitely not the best why to find the device, but works for now
                if ((bool)(device?.Info?.Manufacturer?.Contains("sonos", StringComparison.InvariantCultureIgnoreCase)))
                {
                    //confirm by trying to connect on port 1400
                    client.Connect(device.RemoteEndPoint, 1400);
                    client.Close();
                    return true;
                }
            }
            catch
            {
                return false;//ignore the exception
            }
            return false;
        }
    }
}
