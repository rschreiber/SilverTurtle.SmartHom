using SsdpRadar;

namespace SilverTurtle.SmartHome.ConsoleApp
{
    public class SonosDevice
    {
        private readonly SsdpDevice _device;
        public SonosDevice(SsdpDevice device)
        {
            _device = device;
        }

        public string DeviceName => _device.Info.FriendlyName;
    }
}
