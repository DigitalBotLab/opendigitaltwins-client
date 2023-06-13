using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceSimulator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Sample 1: Create device if you didn't have one in Azure IoT Hub, FIRST YOU NEED SPECIFY connectionString first in AzureIoTHub.cs
            //await CreateDeviceIdentity();
           //User passes DeviceID and access key via CMD LINE

            // Sample 2: comment above line and uncomment following line, FIRST YOU NEED SPECIFY connectingString and deviceConnectionString in AzureIoTHub.cs
            await SimulateDeviceToSendD2cAndReceiveD2c(args[0]);
        }

        public static async Task CreateDeviceIdentity()
        {
            //string deviceName = "thermostat67";
            //await AzureIoTHub.CreateDeviceIdentityAsync(deviceName);
            //Console.WriteLine($"Device with name '{deviceName}' was created/retrieved successfully");
        }

        private static async Task SimulateDeviceToSendD2cAndReceiveD2c(string connString)
        {
            //var tokenSource = new CancellationTokenSource();

            //Console.CancelKeyPress += (s, e) =>
            //{
            //    e.Cancel = true;
            //    tokenSource.Cancel();
            //    Console.WriteLine("Exiting...");
            //};
            //Console.WriteLine("Press CTRL+C to exit");

            //await Task.WhenAll(
            //    AzureIoTHub.SendDeviceToCloudMessageAsync(connString, tokenSource.Token));
            //    //AzureIoTHub.ReceiveMessagesFromDeviceAsync(tokenSource.Token));

            //tokenSource.Dispose();
        }
    }
}
