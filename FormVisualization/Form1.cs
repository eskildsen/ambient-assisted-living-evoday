using OpenCare.EVODAY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace FormVisualization
{
    public partial class Form1 : Form
    {
        private MqttClient MQTTClient;
        private DataTable data;

        private const string COLUMN_TIME = "Tid";
        private const string COLUMN_MESSAGE = "Besked";
        private const string COLUMN_PATIENT = "Patient";

        private Thread mqttWorker;
        private bool invokeInProgress = false;
        private bool isClosing = false;

        public Form1()
        {
            InitializeComponent();
            InitializeDataView();
        }

        private void InitializeDataView()
        {
            data = new DataTable();
            data.Columns.Add(COLUMN_TIME);
            data.Columns.Add(COLUMN_MESSAGE);
            data.Columns.Add(COLUMN_PATIENT);

            dataView.DataSource = this.data;
            // dataView.AutoGenerateColumns = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mqttWorker = new Thread(new ThreadStart(InitMqttClient));
            mqttWorker.Start();
        }

        private void InitMqttClient()
        {
            DisplayLogMessage("Initializing MQTT client");

            var server = "aalserver.au.dk";
            var port = 8883;

            this.MQTTClient = new MqttClient(server, port, true, null, null, MqttSslProtocols.TLSv1_2);

            MQTTClient.MqttMsgPublishReceived += MqttMsgPublishReceived;
            /*MQTTClient.MqttMsgSubscribed += Client_MqttMsgSubscribed;
            MQTTClient.MqttMsgUnsubscribed += Client_MqttMsgUnsubscribed;
            MQTTClient.MqttMsgPublished += Client_MqttMsgPublished;*/
            MQTTClient.ConnectionClosed += MqttConnectionClosed;

            MQTTClient.Subscribe(new string[] { "home0", "home0", "home0" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });


            ConnectWithRetry();
        }

        private void ConnectWithRetry()
        {
            try
            {
                DisplayLogMessage("Connecting...");
                var user = "aal";
                var pass = "aal";
                var keepAlive = (ushort)10;
                var clientId = Guid.NewGuid().ToString();
                DisplayLogMessage("Using client ID: " + clientId);
                
                var state = MQTTClient.Connect(clientId, user, pass, true, keepAlive);
                DisplayLogMessage("State: " + state.ToString());

                DisplayLogMessage("Connected: " + MQTTClient.IsConnected.ToString());

                if(!MQTTClient.IsConnected)
                {
                    Thread.Sleep(2000);
                    ConnectWithRetry();
                }
            }
            catch (Exception)
            {
                DisplayLogMessage("Failed to connect, retrying in 5 seconds...");
                Thread.Sleep(5000);
                ConnectWithRetry();
            }
        }

        private void DisplayLogMessage(string msg)
        {
            if (!logOutput.InvokeRequired)
            {
                logOutput.AppendText(msg + Environment.NewLine);
                return;
            }

            if (!isClosing)
            {
                invokeInProgress = true;
                logOutput.Invoke((Action)(() =>
                {
                    logOutput.AppendText(msg + Environment.NewLine);
                }));
                invokeInProgress = false;
            }
            
        }

        private void MqttConnectionClosed(object sender, EventArgs e)
        {
            DisplayLogMessage("MQTT connection closed event. Connection state: " + MQTTClient.IsConnected.ToString());

        }


        /// <summary>
        /// Handler for receiving data from the MQTT server. This is where the main functionality is placed.
        /// It simplay grabs an MQTT message, tries to parse it into a HEUCOD BasicEvent.
        /// If sucecsful the data is stored in the datastore, providing it with the current time stamp.
        /// Please note, this could cause expections if many services try to create events simulatanously.
        /// If this happens, a random back-off and wait algorithm needs to be implented.
        /// As an alternative GUID could be used as identifier. However, this has twice the length of the 
        /// current long - and it does not provide the same sequence which is supplied by using DateTime.UtcNow.Ticks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            var raw = Encoding.UTF8.GetString(e.Message);

            try
            {
                var ev = ParseEvent(e.Message);
                DisplayLogMessage("MQTT msg: " + raw);
                
                var item = new ListViewItem(new string[]{
                    ev.TimeStampNet.ToLongTimeString(),
                    ev.PatientId,
                    ev.SensorId,
                    ev.SensorType,
                    ev.EventType,
                    (ev.Value.ToString() + " " + ev.Unit).Trim(' '),
                    (ev.Value2.ToString() + " " + ev.Unit2).Trim(' '),
                    (ev.Value3.ToString() + " " + ev.Unit3).Trim(' ')
                });
                

                dataView.Invoke((Action)(() =>
                {
                    listView1.Items.Add(item);
                }));
            }
            catch (Exception ex)
            {
                DisplayLogMessage("Failed to parse event, got error " + ex + ". Event: " + raw);
            }
        }

        private BasicEvent ParseEvent(byte[] message)
        {
                return BasicEvent.FromJson(Encoding.UTF8.GetString(message));
        }

        private async void Form1_FormClosingAsync(object sender, FormClosingEventArgs e)
        {
            isClosing = true;

            if(MQTTClient.IsConnected)
            {
                DisplayLogMessage("Cleaning up connection");
                MQTTClient.Disconnect();
            }

            await Task.Factory.StartNew(() =>
            {
                while (invokeInProgress) ;
            });
        }
    }
}
