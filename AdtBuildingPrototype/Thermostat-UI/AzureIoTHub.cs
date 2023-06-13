using Azure.Messaging.EventHubs.Consumer;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Common.Exceptions;
using System;
using System.Linq;
using System.Collections;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Thermostat_UI
{
    public class AzureIoTHub
    {
        /// <summary>
        /// Please replace with correct connection string value
        /// The connection string could be got from Azure IoT Hub -> Shared access policies -> iothubowner -> Connection String:
        /// </summary>
        private string iotHubConnectionString = "";

        public string IotHubConnectionString { get => iotHubConnectionString; set => iotHubConnectionString=value; }
        
        /// <summary>
        /// Please replace with correct device connection string
        /// The device connect string could be got from Azure IoT Hub -> Devices -> {your device name } -> Connection string
        /// </summary>
        private string deviceConnectionString ="";

        public string DeviceConnectionString { get => deviceConnectionString; set => deviceConnectionString=value; }


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



        public async Task SendDeviceToCloudMessageAsync(double temperature)
        {
            var deviceClient = DeviceClient.CreateFromConnectionString(deviceConnectionString);

            string[] keyValuePairs = deviceConnectionString.Split(';');
            string deviceId = "";

            foreach (string keyValue in keyValuePairs)
            {
                string[] parts = keyValue.Split('=');
                if (parts[0].Equals("DeviceId"))
                {
                    deviceId = parts[1];
                    break;
                }
            }

            //double avgTemperature = 70.0D;
            //var rand = new Random();

            //double currentTemperature = avgTemperature + rand.NextDouble() * 4 - 3;

            var telemetryDataPoint = new
            {
                Temperature = temperature
            };
            var messageString = JsonSerializer.Serialize(telemetryDataPoint);
            var message = new Microsoft.Azure.Devices.Client.Message(Encoding.UTF8.GetBytes(messageString))
            {
                ContentType = "application/json",
                ContentEncoding = "utf-8"
            };
            await deviceClient.SendEventAsync(message);
            Console.WriteLine($"{DateTime.Now} > Sending message: {deviceId}:{messageString}");

            //Keep this value above 1000 to keep a safe buffer above the ADT service limits
            //See https://aka.ms/adt-limits for more info
            await Task.Delay(5000);
            
        }

        public async Task<string> ReceiveCloudToDeviceMessageAsync()
        {
            var oneSecond = TimeSpan.FromSeconds(1);
            var deviceClient = DeviceClient.CreateFromConnectionString(deviceConnectionString);

            while (true)
            {
                var receivedMessage = await deviceClient.ReceiveAsync();
                if (receivedMessage == null)
                {
                    await Task.Delay(oneSecond);
                    continue;
                }

                var messageData = Encoding.ASCII.GetString(receivedMessage.GetBytes());
                await deviceClient.CompleteAsync(receivedMessage);
                return messageData;
            }
        }

        public async Task ReceiveMessagesFromDeviceAsync(CancellationToken cancelToken)
        {
            try
            {
                string eventHubConnectionString = await IotHubConnection.GetEventHubsConnectionStringAsync(iotHubConnectionString);
                await using var consumerClient = new EventHubConsumerClient(
                    EventHubConsumerClient.DefaultConsumerGroupName,
                    eventHubConnectionString);

                await foreach (PartitionEvent partitionEvent in consumerClient.ReadEventsAsync(cancelToken))
                {
                    if (partitionEvent.Data == null) continue;

                    string data = Encoding.UTF8.GetString(partitionEvent.Data.Body.ToArray());
                    Console.WriteLine($"Message received. Partition: {partitionEvent.Partition.PartitionId} Data: '{data}'");
                }
            }
            catch (TaskCanceledException) { } // do nothing
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading event: {ex}");
            }
        }
    }
}
