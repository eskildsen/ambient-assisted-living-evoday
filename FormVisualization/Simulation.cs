using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormVisualization
{
    class Simulation
    {
        private HUECOD mqtt;

        public void setMqttConnection(HUECOD huecod)
        {
            mqtt = huecod;
        }

        private void sendEvent(string json)
        {
            if(mqtt != null && mqtt.IsConnected)
            {
                mqtt.Publish(json);
            }
        }

        public void noise()
        {
            sendEvent(SimulationEvent.StepCounter);
            sendEvent(SimulationEvent.UserIsHome);
        }

        public void toilet()
        {
            sendEvent(SimulationEvent.ToiletOcupied);
        }

        public void  toiletFlush()
        {

        }

        public void handWash()
        {
            sendEvent(SimulationEvent.HandWash);
        }

        public void soap()
        {
            sendEvent(SimulationEvent.Soap);
        }

        public void toothbrush()
        {
        }

        public void leaveRoom()
        {
            sendEvent(SimulationEvent.UserIsHome2);
        }
    }
}
