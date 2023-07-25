// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using Azure;
using Azure.Core.Pipeline;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using Azure.Messaging.EventGrid;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BuildingFunctionsApp
{

    public class Operation
    {
        public string Op { get; set; }
        public string Path { get; set; }
        public int Value { get; set; }
    }

    // This class processes property change notification from ADT, reads twin room id that's associated to the event,
    // finds the parent floor twin that contains this twin and sets the parent Temperature property
    // to the value from the notification.
    public static class ProcessDTRoutedData
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static string adtServiceUrl = Environment.GetEnvironmentVariable("ADT_SERVICE_URL");

        [FunctionName("ProcessDTRoutedData")]
        public static async Task Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {
            log.LogInformation("Start execution");

            DigitalTwinsClient client;
            // Authenticate on ADT APIs
            try
            {
                var credentials = new DefaultAzureCredential();
                client = new DigitalTwinsClient(new Uri(adtServiceUrl), credentials, new DigitalTwinsClientOptions { Transport = new HttpClientTransport(httpClient) });
                log.LogInformation("ADT service client connection created.");
            }
            catch (Exception e)
            {
                log.LogError($"ADT service client connection failed. {e}");
                return;
            }

            if (client != null)
            {
                if (eventGridEvent != null && eventGridEvent.Data != null)
                {
                    string twinId = eventGridEvent.Subject.ToString();
                    JObject message = (JObject)JsonConvert.DeserializeObject(eventGridEvent.Data.ToString());

                    log.LogInformation($"Reading event from {twinId}: {eventGridEvent.EventType}: {message["data"]}");

                    //Find and update parent Twin
                    string parentId = await AdtUtilities.FindParentAsync(client, twinId, "contains", log);
                    if (parentId != null)
                    {
                        if (parentId.Contains("room"))
                        {
                            //Only if we are dealing with a Room now, get the temp
                            int temp = await AdtUtilities.GetRoomTemperatureAsync(client, parentId, log);
                            log.LogInformation($"Room found {parentId} temperature: {temp}");

                            try
                            {
                                var operations = JsonConvert.DeserializeObject<Operation[]>(message["data"].ToString());

                                foreach (var operation in operations)
                                {
                                    log.LogInformation("Operation: " + operation.Op);
                                    log.LogInformation("Path: " + operation.Path);
                                    log.LogInformation("Value: " + operation.Value);

                                    if (operation.Path.Equals("/Temperature") && operation.Value != temp)
                                    {
                                        //Temp has changed, Update the Room
                                        await AdtUtilities.UpdateTwinPropertyAsync(client, parentId, operation.Path, float.Parse(operation.Value.ToString()), log);
                                    }
                                    else
                                    {
                                        log.LogInformation($"Update skipped: {parentId} no temperature change: {temp}");
                                    }
                                }
                            }
                            catch (JsonException ex)
                            {
                                log.LogError("Error parsing JSON: " + ex.Message);
                            }
                        }
                    }
                }
            }
        }
    }
}