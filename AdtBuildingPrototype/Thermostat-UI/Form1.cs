using Microsoft.Azure.Devices;
using System.Text.RegularExpressions;
using System.Timers;

namespace Thermostat_UI
{
    public partial class Form1 : Form
    {
        // Define variables for the fields
        string connectionString = null;
        string hostName = null;
        string deviceId = null;
        string sharedAccessKey = null;

        AzureIoTHub hub;

        public Form1(string[] args)
        {
            InitializeComponent();

            //Parse the args
            //HostName=gavin-iot-hub.azure-devices.net; DeviceId=thermostat1-1; SharedAccessKey=wA+XRvz2r2i21aOArj5seFJV3WU=
            connectionString = args[0];

            // Split the connection string into key-value pairs
            string[] parts = connectionString.Split(new char[] { ';', '=' }, StringSplitOptions.RemoveEmptyEntries);

            // Loop through the key-value pairs and assign values to the variables
            for (int i = 0; i < parts.Length; i += 2)
            {
                switch (parts[i].ToLower())
                {
                    case "hostname":
                        hostName = parts[i + 1];
                        break;
                    case "deviceid":
                        deviceId = parts[i + 1];
                        break;
                    case "sharedaccesskey":
                        sharedAccessKey = parts[i + 1];
                        break;
                }
            }


            //Set UI labels
            Text=deviceId;

        }

        private void Log(string message)
        {
            // Append the new log entry to the existing text in the TextBox
            txtLog.AppendText(DateTime.Now.ToString() + " " + message + "\r\n");

            // Scroll to the end of the TextBox to show the latest log entry
            txtLog.ScrollToCaret();
        }

        private static async Task SimulateDeviceToSendD2cAndReceiveD2c(string connString)
        {
            var tokenSource = new CancellationTokenSource();

            AzureIoTHub hub = new AzureIoTHub();
            hub.IotHubConnectionString = connString;

            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                tokenSource.Cancel();
                Console.WriteLine("Exiting...");
            };
            Console.WriteLine("Press CTRL+C to exit");

            await Task.WhenAll(
                hub.SendDeviceToCloudMessageAsync(65),
                hub.ReceiveMessagesFromDeviceAsync(tokenSource.Token));

            tokenSource.Dispose();
        }


        private async void Form1_Load(object sender, EventArgs e)
        {
            hub = new AzureIoTHub();
            hub.DeviceConnectionString = connectionString;

            // Start the timer
            timer1.Start();
        }

        async Task timer1_Tick()
        {
            int Temperature = 0;

            if (chkRandom.Checked)
            {
                Random random = new Random();
                Temperature = random.Next(60, 91); // Generates a random number between 60 (inclusive) and 91 (exclusive)
                trackBar1.Value = Temperature;
            }
            else
            {
                Temperature = trackBar1.Value;
            }

            // Send the message
            Log(String.Format("Sending temp...{0}", Temperature));
            await hub.SendDeviceToCloudMessageAsync((double)Temperature);
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {

            // Call the asynchronous method using await
            await timer1_Tick();

            // You can also catch any exceptions thrown by the async method
            // using try/catch blocks as you would in regular synchronous code
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}

// Define a custom comparer for sorting the items
class ItemComparer : IComparer<string>
{
    public int Compare(string x, string y)
    {
        string[] xParts = x.Split('-');
        string[] yParts = y.Split('-');

        int x1 = int.Parse(xParts[1]);
        int y1 = int.Parse(yParts[1]);

        return x1.CompareTo(y1);
    }
}
