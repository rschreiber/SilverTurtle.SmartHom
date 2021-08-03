using System.Threading.Tasks;

namespace SilverTurtle.SmartHome.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {

            SonosDeviceLocator locator = new();
            var devices = await locator.FindDevicesAsync();
            foreach (var device in devices)
            {
                System.Console.WriteLine(device.DeviceName);
            }
        }
    }
}
