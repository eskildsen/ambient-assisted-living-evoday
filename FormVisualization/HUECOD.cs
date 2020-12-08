using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using static uPLibrary.Networking.M2Mqtt.MqttClient;

namespace FormVisualization
{
    class HUECOD
    {
        private string server = "aalserver.au.dk";
        private int port = 8883;
        private string user = "aal";
        private string password = "aal";
        private ushort keepAlive = 10;
        private string topic = "home0";
        private MqttClient client;

        public readonly string clientId = Guid.NewGuid().ToString();
        public event MqttMsgPublishEventHandler MqttMsgPublishReceived;
        public event ConnectionClosedEventHandler ConnectionClosed;
        public event ConnectionStatusChangedEventHandler ConnectionStatusChanged;

        public delegate void ConnectionStatusChangedEventHandler(object sender, StatusChangeEventArgs args);

        public HUECOD()
        {
            client = new MqttClient(server, port, true, null, null, MqttSslProtocols.TLSv1_2);
        }

        public void Publish(string json)
        {
            client.Publish(topic, Encoding.UTF8.GetBytes(json),
								MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE,
								true);
        }

        public bool IsConnected
        {
            get
            {
                return client.IsConnected;
            }
        }

        public void Subscribe()
        {
            client.MqttMsgPublishReceived += MqttMsgPublishReceived;
            client.ConnectionClosed += ConnectionClosed;
            client.ConnectionClosed += OnConnectionClosed;

            client.Subscribe(new string[] { "home0", "home0", "home0" }, new byte[] {
                MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
                MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
                MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE
            });
        }

        public void ConnectWithRetry()
        {
            try
            {
                EmitStatus("Forbinder...");
                client.Connect(clientId, user, password, true, keepAlive);

                if (client.IsConnected)
                {
                    EmitStatus("Forbundet");
                }
                else
                {
                    EmitStatus("Ikke forbundet");
                    Thread.Sleep(2000);
                    ConnectWithRetry();
                }
            }
            catch (Exception)
            {
                EmitStatus("Forbindelse fejlede");
                Thread.Sleep(5000);
                ConnectWithRetry();
            }
        }

        public void Disconnect()
        {
            client.Disconnect();
        }

        private void EmitStatus(string status)
        {
            ConnectionStatusChanged?.Invoke(this, new StatusChangeEventArgs(status));
        }

        private void OnConnectionClosed(object sender, EventArgs e)
        {
            EmitStatus("Forbindelsen blev lukket");
        }
    }

    public class StatusChangeEventArgs : EventArgs
    {
        private readonly string status;

        public StatusChangeEventArgs(string status)
        {
            this.status = status;
        }

        public string Status
        {
            get { return this.status; }
        }
    }
}
