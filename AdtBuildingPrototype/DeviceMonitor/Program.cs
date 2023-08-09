using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceMonitor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Sample 2: comment above line and uncomment following line, FIRST YOU NEED SPECIFY connectingString and deviceConnectionString in AzureIoTHub.cs
            await ReceiveD2c();
        }

        private static async Task ReceiveD2c()
        {
            var tokenSource = new CancellationTokenSource();
            var connString = ConfigurationManager.AppSettings["iotHub"];

            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                tokenSource.Cancel();
                Console.WriteLine("Exiting...");
            };
            Console.WriteLine("Press CTRL+C to exit");

            await Task.WhenAll(
                //AzureIoTHub.SendDeviceToCloudMessageAsync(tokenSource.Token),
                AzureIoTHub.ReceiveMessagesFromDeviceAsync(connString, tokenSource.Token));

            tokenSource.Dispose();
        }
    }
}