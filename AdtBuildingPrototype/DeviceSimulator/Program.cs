using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceSimulator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Sample 2: comment above line and uncomment following line, FIRST YOU NEED SPECIFY connectingString and deviceConnectionString in AzureIoTHub.cs
            await ReceiveD2c("");
        }

        private static async Task ReceiveD2c(string connString)
        {
            var tokenSource = new CancellationTokenSource();

            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                tokenSource.Cancel();
                Console.WriteLine("Exiting...");
            };
            Console.WriteLine("Press CTRL+C to exit");

            await Task.WhenAll(
                //AzureIoTHub.SendDeviceToCloudMessageAsync(tokenSource.Token),
                AzureIoTHub.ReceiveMessagesFromDeviceAsync(tokenSource.Token));

            tokenSource.Dispose();
        }
    }
}