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
        private HUECOD huecod;

        private const string COLUMN_TIME = "Tid";
        private const string COLUMN_MESSAGE = "Besked";
        private const string COLUMN_PATIENT = "Patient";

        private Thread mqttWorker;
        private bool invokeInProgress = false;
        private bool isClosing = false;

        private SmartMirrorController eventHandler;

        private Simulation simulator = new Simulation();

        public Form1()
        {
            InitializeComponent();
        }

        private void EventHandler_IconsUpdated(object sender, EventArgs e)
        {
            ToggleIcons();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            eventHandler = new SmartMirrorController();
            eventHandler.IconsUpdated += EventHandler_IconsUpdated;
            mqttWorker = new Thread(new ThreadStart(InitMqttClient));
            mqttWorker.Start();
            ToggleIcons();
        }

        private void InitMqttClient()
        {
            DisplayLogMessage("Initializing MQTT client");

            huecod = new HUECOD();
            huecod.ConnectionStatusChanged += Huecod_ConnectionStatusChanged;
            huecod.MqttMsgPublishReceived += Huecod_MsgReceived;
            huecod.Subscribe();
            huecod.ConnectWithRetry();

            simulator.setMqttConnection(huecod);

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
        private void Huecod_MsgReceived(object sender, MqttMsgPublishEventArgs e)
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
                

                listView1.Invoke((Action)(() =>
                {
                    listView1.Items.Add(item);
                }));

                switch (ev.EventType)
                {
                    case "OpenCare.EVODAY.EDL.ToiletOccupancy":
                        eventHandler.ToiletEvent(ev);
                        ToggleIcons();
                        break;
                    case "OpenCare.EVODAY.EDL.HandWashingEvent":
                        eventHandler.HandwashEvent(ev);
                        ToggleIcons();
                        break;
                    case "OpenCare.EVODAY.EDL.SoapDispensnig":
                        eventHandler.SoapEvent(ev);
                        ToggleIcons();
                        break;
                }
            }
            catch (Exception ex)
            {
                DisplayLogMessage("Failed to parse event, got error " + ex + ". Event: " + raw);
            }
        }

        private void ToggleIcons()
        {
            Invoke((Action)(() =>
            {
                iconHandwash.Visible = eventHandler.HandwashIcon;
                iconSoap.Visible = eventHandler.SoapIcon;
                iconToothbrush.Visible = eventHandler.ToothbrushIcon;
                iconHandwash.Refresh();
                iconHandwash.Update();
                iconSoap.Refresh();
                iconSoap.Update();
                iconToothbrush.Refresh();
                iconToothbrush.Update();
            }));
        }

        private BasicEvent ParseEvent(byte[] message)
        {
                return BasicEvent.FromJson(Encoding.UTF8.GetString(message));
        }

        private async void Form1_FormClosingAsync(object sender, FormClosingEventArgs e)
        {
            isClosing = true;

            if(huecod.IsConnected)
            {
                DisplayLogMessage("Closing connection");
                huecod.Disconnect();
            }

            await Task.Factory.StartNew(() =>
            {
                while (invokeInProgress) ;
            });
        }

        private void Huecod_ConnectionStatusChanged(object sender, StatusChangeEventArgs args)
        {
            if (!labelConnectionStatus.InvokeRequired)
            {
                labelConnectionStatus.Text = args.Status;
                return;
            }

            if (!isClosing)
            {
                Invoke((Action)(() =>
                {
                    labelConnectionStatus.Text = args.Status;
                }));
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

        private void btnSimulateToilet_Click(object sender, EventArgs e)
        {
            simulator.toilet();
        }

        private void btnSimulateFlush_Click(object sender, EventArgs e)
        {
            logOutput.Clear();
            listView1.Items.Clear();
            eventHandler.Reset();
        }

        private void btnSimulateHandWashing_Click(object sender, EventArgs e)
        {
            simulator.handWash();
        }

        private void btnSimulateSoap_Click(object sender, EventArgs e)
        {
            simulator.soap();
        }

        private void btnSimulateToothbrush_Click(object sender, EventArgs e)
        {
            simulator.toothbrush();
        }

        private void btnSimulateLeaveRoom_Click(object sender, EventArgs e)
        {
            simulator.leaveRoom();
        }

        private void iconContainer_Click(object sender, EventArgs e)
        {
            if (isIconsFullScreen())
                restoreOriginalLayoutSizes();
            else
                makeIconsFullScreen();
        }

        private bool isIconsFullScreen()
        {
            return !tableLayoutPanel1.Visible;
        }

        private void restoreOriginalLayoutSizes()
        {
            tableLayoutPanel1.Controls.Add(iconContainer);
            tableLayoutPanel1.Show();
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void makeIconsFullScreen()
        {
            tableLayoutPanel1.Hide();
            Controls.Add(iconContainer);
            this.FormBorderStyle = FormBorderStyle.None;
        }
    }
}
