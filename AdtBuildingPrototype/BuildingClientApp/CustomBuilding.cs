using System.Threading.Tasks;

namespace BuildingClientApp
{
    public class CustomBuilding
    {
        private readonly CommandLoop cl;
        public CustomBuilding(CommandLoop cl)
        {
            this.cl = cl;
        }

        public async Task InitBuilding(string location, int floors, int rooms)
        {
            Log.Alert($"Deleting all twins...");
            await cl.DeleteAllTwinsAsync();
            Log.Out($"Creating {floors} floors, {rooms} rooms and {rooms} thermostats in {location}");
            await InitializeGraph(location, floors, rooms);
        }

        private async Task InitializeGraph(string location, int floors, int rooms)
        {
            string[] modelsToUpload = new string[5] {"CreateModels", "Building", "Floor", "ThermostatModel", "RoomModel" };
            Log.Out($"Uploading {string.Join(", ", modelsToUpload)} models");

            await cl.CommandCreateModels(modelsToUpload);

            Log.Out($"Creating Building, Floors, Rooms and Thermostats...");

            string buildingId = string.Format("building1");
            string bldName = location + " building.";

            await cl.CommandCreateDigitalTwin(new string[9]
            {
                "CreateTwin", "dtmi:digitalbotlab:DigitalTwins:Building;1", buildingId,
                "DisplayName", "string", bldName,
                "Location", "string", location
            });

            //For each Floor
            for (int i = 1; i <= floors; i++)
            {
                string floorId = string.Format("floor{0}", i);
                string floorName = string.Format("Floor {0}", i);
                string relBF = string.Format("{0}_to_{1}_edge", buildingId, floorId);

                await cl.CommandCreateDigitalTwin(new string[9]
                {
                    "CreateTwin", "dtmi:digitalbotlab:DigitalTwins:Floor;1", floorId,
                    "DisplayName", "string", floorName,
                    "FloorNum", "integer", i.ToString()
                });

                //Associate Floor to Building
                await cl.CommandCreateRelationship(new string[]
                {
                    "CreateEdge", buildingId, "contains", floorId, relBF,
                    "ownershipUser", "string", "BotLab",
                    "ownershipDepartment", "string", "Twins Division"
                });

                //Create Rooms
                for (int j = 1; j <= rooms; j++)
                {
                    string roomId = string.Format("room{0}-{1}", i,j);
                    string roomName = string.Format("Room {0}-{1}", i,j);

                    string thermoId = string.Format("thermostat{0}-{1}",i,j);
                    string thermoName = string.Format("Thermostat {0}-{1}",i,j);
                    string rel1 = string.Format("{0}_to_{1}_edge", floorId, roomId);
                    string rel2 = string.Format("{0}-{1}_to_{2}_edge", floorId, roomId, thermoId);

                    await cl.CommandCreateDigitalTwin(new string[]
                    {
                        "CreateTwin", "dtmi:digitalbotlab:DigitalTwins:Room;1", roomId,
                        "DisplayName", "string", roomName,
                    });

                    await cl.CommandCreateDigitalTwin(new string[]
                    {
                        "CreateTwin", "dtmi:digitalbotlab:DigitalTwins:Thermostat;1", thermoId,
                        "DisplayName", "string", thermoName,
                        "FirmwareVersion", "string", "1.3.9",
                        "Temperature", "double", "75",
                    });

                    Log.Out($"Creating edges between the Floor and Room");

                    await cl.CommandCreateRelationship(new string[]
                    {
                        "CreateEdge", floorId, "contains", roomId, rel1,
                        "ownershipUser", "string", "BotLab",
                        "ownershipDepartment", "string", "Twins Division"
                    });

                    await cl.CommandCreateRelationship(new string[]
                    {
                        "CreateEdge", roomId, "contains", thermoId, rel2,
                        "ownershipUser", "string", "BotLab",
                        "ownershipDepartment", "string", "Twins Division"
                    });
                }
            }
        }
    }
}
