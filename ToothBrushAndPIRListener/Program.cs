/*SF
Copyright(c) 2020, Stefan Wagner, Aarhus University, Aarhus, Denmark
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
    * Redistributions of source code must retain the above copyright
      notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright
      notice, this list of conditions and the following disclaimer in the
      documentation and/or other materials provided with the distribution.
    * Neither the name of Aarhus University nor the
      names of its contributors may be used to endorse or promote products
      derived from this software without specific prior written permission.
    * All source code, academic papers, technical respots, web pages or using
      or citing this source code must cite the following papers: 
      1) "The Healthcare Equipment Usage and Context Data (HEUCOD) Standard"
      2) "Healthcare Equipment Usage and Context Data (HEUCOD) Referece Implementation" 
      3) "The OpenCare Framework", all by S. Wagner and J. Miranda.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED.IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using M2Mqtt;
using M2Mqtt.Messages;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OpenCare.EVODAY;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HEUCODtoMongoPersistenceService
{

    #region TEST CODE
    //A basic test enitity
    public class Entity
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
    #endregion

    /// <summary>
    /// This is a demo program which listens for relevant MQTT events for 
    /// a toothbrush and a movement sensor in the bathroom
    /// a relation database - but rather store data in a tuple object datastore from
    /// which relevant information can be extracted - without any prior knowledege of the 
    /// deployment, 
    /// Other persistence services can exists side by side - e.g. using a normalized relational 
    /// database design - which requires more administration to maintain and initialize.
    /// This service can either be activated by a HEUCOD control script - or it can 
    /// be made to run as windows service or as a docker service. 
    /// </summary>
    class Program 
    {
        private string ClientID;
        private string CLIENT_IDENTIFIER;

        public byte MQTTState { get; private set; }
        public MqttClient MQTTClient { get; private set; }
        public MongoClient Client { get; private set; }
        public IMongoDatabase Database { get; private set; }
        public IMongoCollection<Entity> DBC2 { get; private set; }
        public IMongoCollection<BasicEvent> DBC { get; private set; }
        public object DBCollection { get; private set; }
        public Timer timer { get; private set; }
        public DateTime LastEvent { get; private set; }
        public static DateTime ApplicationStartedTime { get; private set; }
        public string LastEventMessage { get; private set; }
        public BasicEvent LastEventReceived { get; private set; }

        static void Main(string[] args)
        {
            Console.WriteLine("ToothBrushAndPIRListenerService is running !");


            //This is the time when the application was started
            ApplicationStartedTime = DateTime.Now;

            try
            {
                var s = new Program();
                s.InitLogger();
                s.InitizlizeMqttClient();
            
            } 
            catch (Exception ex)         
            {
                Console.WriteLine("An error occurred: " + ex);    
            }

            Console.WriteLine("Hit ENTER to end");
            Console.ReadLine();
        }

        /// <summary>
        /// Will initialize the logger
        /// </summary>
        private void InitLogger()
        {
             Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs.txt", rollingInterval: Serilog.RollingInterval.Month)
            .CreateLogger();
        }

        private void RestartServer()
        {

            try {

                Log.Logger.Information("Preparing to restart server");

                CleanUpMQTT();

                var cmd = new System.Diagnostics.ProcessStartInfo("shutdown.exe", "-r -t 5");
                cmd.CreateNoWindow = true;
                cmd.UseShellExecute = false;
                cmd.ErrorDialog = false;
                System.Diagnostics.Process.Start(cmd);

            } 
            catch (Exception ex)
            {
                Log.Logger.Error("Error during restart: " + ex);
            }
        }

        /// <summary>
        /// This will check the MQTT activity - and if it doesnt run -
        /// it will restart the 
        /// </summary>
        /// <param name="state"></param>
        private void CheckMQTTActivity(object state)
        {
            if ((DateTime.Now - LastEvent).TotalMinutes > 3)
            {
                Console.WriteLine("No actiity for the last 3 minutes - restarting the MQTT service");
                InitizlizeMqttClient();
            }

            var timeSinceStart = (DateTime.Now - ApplicationStartedTime).TotalMinutes;

            if (timeSinceStart > 1434 && DateTime.Now.Hour == 3) //if it is 24*60-6 minutes ago - and it is then restart
                RestartServer();
        }


        private bool CallBack(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        /// <summary>
        /// Prepare to listen for events via MMQT
        /// </summary>
        private void InitizlizeMqttClient()
        {
            try
            {

                if (MQTTClient != null)
                    CleanUpMQTT();

                if (MQTTClient == null)
                {
                    
                    MQTTClient = new MqttClient(Setting.Default.MQTTServer, Setting.Default.MQTTPort, true, null, null, MqttSslProtocols.TLSv1_2,  CallBack);
                    //c = new MqttClient , Int32.Parse(Port.Text), false, MqttSslProtocols.None, null, null);

                    // register to message received
                    MQTTClient.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
                    MQTTClient.MqttMsgSubscribed += Client_MqttMsgSubscribed;
                    MQTTClient.MqttMsgUnsubscribed += Client_MqttMsgUnsubscribed;
                    MQTTClient.MqttMsgPublished += Client_MqttMsgPublished;
                    MQTTClient.ConnectionClosed += MQTTClient_ConnectionClosed;

                    // subscribe to the topics PIRSensor and BEDSensor .... and then Stefans own Test message
                    MQTTClient.Subscribe(new string[] { "home0", "home0" , "home0" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE,  MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE});
                    
                    ClientID = Guid.NewGuid().ToString();
                    CLIENT_IDENTIFIER = ClientID;// + Guid.NewGuid(); - consider giving it something else then a GUID - but then again - need to persist it per client.

                    DoMqttConnect();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log.Logger.Error("Error during InitizlizeMqttClient: " + ex.Message);
            }

        }

        //Cleans up after MQTT connections. First, it disconnects if it is still connected. Next it unsubsribes all events
        private void CleanUpMQTT()
        {
            try
            {
                // deregister event listeners
                if (MQTTClient != null)
                { 
                    
                    if (MQTTClient.IsConnected) MQTTClient.Disconnect();

                    MQTTClient.MqttMsgPublishReceived -= Client_MqttMsgPublishReceived;
                    MQTTClient.MqttMsgSubscribed -= Client_MqttMsgSubscribed;
                    MQTTClient.MqttMsgUnsubscribed -= Client_MqttMsgUnsubscribed;
                    MQTTClient.MqttMsgPublished -= Client_MqttMsgPublished;
                    MQTTClient.ConnectionClosed -= MQTTClient_ConnectionClosed;
                    MQTTClient = null;
                }
            }
            catch (Exception e)
            {
                Log.Logger.Error("Error during clean up: " + e);
                Console.WriteLine("Error during clean up: " + e);
            }
        }


        private void DoMqttConnect()
        {
            try
            { 
                if (MQTTClient != null && !MQTTClient.IsConnected)
                {
                    PrintTime();
                    Console.WriteLine("Connecting to MQTT server");
                    Log.Logger.Information("Connecting to MQTT server");
                    MQTTState = MQTTClient.Connect(CLIENT_IDENTIFIER, Setting.Default.MQTTUser, Setting.Default.MQTTPass, true, Setting.Default.KeepAlive);
                    Console.WriteLine("Connect called ");
                    Console.WriteLine("State: ");
                    Console.Write(MQTTState);

                    Log.Logger.Information("---> Connect called");
                } 
                else
                {
                    PrintTime();
                    Console.WriteLine("Tried connecting to MQTT server - but was already connected");
                    Log.Logger.Information("Tried connecting to MQTT server - but was already connected");
                }

            }
            catch (Exception e)
            {
                PrintTime();

                Console.WriteLine("Error while connecting. Retrying again in 5 seconds: "+e);
                Log.Logger.Error("Error while connecting. Retrying again in 5 seconds: "+e);
                Thread.Sleep(5000);
            }

        }

        private void PrintTime()
        { 
            Console.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
        }

        private void MQTTClient_ConnectionClosed(object sender, EventArgs e)
        {
            try
            {

                InitizlizeMqttClient();
            }
            catch (Exception ez)
            {
                Console.WriteLine("Error while initializing: " + ez);
                Log.Logger.Error("Error while  while initializing MQTT stack: " + ez);
            }

        }
        /// <summary><
        /// Implent this to update the user interface to show it is connected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            //Console.WriteLine("Test");
        }


        private void Client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            Console.WriteLine("Test");
        }

        private void Client_MqttMsgUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {
            //Console.WriteLine("Test");
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
        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {

            LastEvent = DateTime.Now;

            //  newTask.Start(() =>

            try
            {
                var message = (Encoding.UTF8.GetString(e.Message));

                //Check whether this contains a string with patientid - if not - it is not properly formatted - and worthless
                if (message.ToLower().Contains("patientid"))
                {
                    var newevent = BasicEvent.FromJson(message);

                    if (newevent.PatientId == "2")
                    {

                        newevent.Id = DateTime.UtcNow.Ticks.ToString();

                        if (newevent.SensorType.Contains("ToothBrush"))
                            Console.WriteLine("Just detected a toothbrushing event which lasted " + newevent.Value + " seconds - of type: " + newevent.SensorType);
                        if (newevent.SensorType.Contains("Bath") && newevent.Value == 1)
                            Console.WriteLine("Just detected bathroom movement of type: " + newevent.SensorType);
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO: Add logging in case of errors - and also implement a back-off and wait and retry - in case 
                //the ID is already used by the database.
                Console.WriteLine("Error in service: " + ex);
                Log.Logger.Error("Error while reciving event: " + ex);
            }
        }
    }
}
