using Azure.Messaging.EventHubs.Consumer;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Common.Exceptions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingClientApp
{
    public class AzureIoTHub
    {
        /// <summary>
        /// Please replace with correct connection string value
        /// The connection string could be got from Azure IoT Hub -> Shared access policies -> iothubowner -> Connection String:
        /// </summary>
        private string iotHubConnectionString = "";

        public string IotHubConnectionString { get => iotHubConnectionString; set => iotHubConnectionString=value; }
        

        public async Task<IEnumerable<Device>> GetDevices()
        {
            var registryManager = RegistryManager.CreateFromConnectionString(iotHubConnectionString);

            try
            {
                var devices = await registryManager.GetDevicesAsync(100);
                return devices;
            }
            catch (TaskCanceledException) { } // do nothing
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading event: {ex}");
            }
            return Enumerable.Empty<Device>();
        }

        public async Task<Device> CreateDeviceIdentityAsync(string deviceName)
        {
            var registryManager = RegistryManager.CreateFromConnectionString(iotHubConnectionString);
            var device = new Device(deviceName);
            try
            {
                device = await registryManager.AddDeviceAsync(device);
            }
            catch (DeviceAlreadyExistsException)
            {
                device = await registryManager.GetDeviceAsync(deviceName);
            }

            return device;
        }

        public string GetDeviceConnectionString(Device device)
        {
            var parsedConnectionString = Microsoft.Azure.Devices.IotHubConnectionStringBuilder.Create(iotHubConnectionString);

            var hostName = parsedConnectionString.HostName;
            return $"HostName={hostName};DeviceId={device.Id};SharedAccessKey={device.Authentication.SymmetricKey.PrimaryKey}";
        }

        public async Task DeleteDevicesAsync()
        {
            var registryManager = RegistryManager.CreateFromConnectionString(iotHubConnectionString);

            try
            {
                IEnumerable<Device> devs = await registryManager.GetDevicesAsync(100);

                foreach (var dev in devs)
                {
                    await registryManager.RemoveDeviceAsync(dev.Id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading event: {ex}");
            }
            return;
        }

    }
}
