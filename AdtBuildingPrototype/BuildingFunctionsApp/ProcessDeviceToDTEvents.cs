using Azure;
using Azure.Core.Pipeline;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using Azure.Messaging.EventGrid;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace BuildingFunctionsApp
{
    // This class processes telemetry events from IoT Hub, reads temperature of a device
    // and sets the "Temperature" property of the device with the value of the telemetry.
    public class ProcessDeviceToDTEvents
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static string adtServiceUrl = Environment.GetEnvironmentVariable("ADT_SERVICE_URL");

        [FunctionName("ProcessDeviceToDTEvents")]
        public async Task Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
            // After this is deployed, you need to turn the Managed Identity Status to "On",
            // Grab Object Id of the function and assigned "Azure Digital Twins Owner (Preview)" role
            // to this function identity in order for this function to be authorized on ADT APIs.

            //Authenticate with Digital Twins
            var credentials = new DefaultAzureCredential();
            DigitalTwinsClient client = new DigitalTwinsClient(
                new Uri(adtServiceUrl), credentials, new DigitalTwinsClientOptions
                { Transport = new HttpClientTransport(httpClient) });
            log.LogInformation($"ADT service client connection created.");

            if (eventGridEvent != null && eventGridEvent.Data != null)
            {
                log.LogInformation(eventGridEvent.Data.ToString());

                // Reading deviceId and temperature for IoT Hub JSON
                JObject deviceMessage = (JObject)JsonConvert.DeserializeObject(eventGridEvent.Data.ToString());
                string deviceId = (string)deviceMessage["systemProperties"]["iothub-connection-device-id"];

                try
                {
                    if (deviceId == "thermostat1-1")
                    {
                        var temperature = deviceMessage["body"]["temperature"];

                        if (temperature != null)
                        {
                            log.LogInformation($"Device:{deviceId} Temperature is:{temperature}");

                            var cel = temperature.Value<double>();

                            //Convert to Fahrenheit
                            var fr = Math.Round((cel * 9) / 5 + 32);

                            log.LogInformation($"C to F => Temperature is: {fr}");

                            //Update twin using device temperature
                            var updateTwinData = new JsonPatchDocument();
                            updateTwinData.AppendReplace("/Temperature", fr);

                            await client.PublishTelemetryAsync(deviceId, Guid.NewGuid().ToString(), updateTwinData.ToString());
                        }
                        else
                        {
                            log.LogInformation($"No temperature data...");
                        }
                    }
                    else
                    {
                        //Old Way
                        var temperature = deviceMessage["body"]["Temperature"];

                        log.LogInformation($"Device:{deviceId} Temperature is: {temperature}");

                        //Update twin using device temperature
                        var updateTwinData = new JsonPatchDocument();
                        updateTwinData.AppendReplace("/Temperature", temperature.Value<double>());

                        await client.PublishTelemetryAsync(deviceId, Guid.NewGuid().ToString(), updateTwinData.ToString());
                    }
                }
                catch (System.Exception ex)
                {
                    log.LogError(ex.ToString());
                }
            }
        }
    }
}