using OpenCare.EVODAY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FormVisualization
{
    class SmartMirrorController
    {
        public bool ToothbrushIcon = false;
        public bool HandwashIcon = false;
        public bool SoapIcon { get {
                if (HandwashIcon) return false;
                else return _soapIcon;
            } }
        public bool _soapIcon = false;

        private Timer toiletTimer;
        private Timer handwashStopTimer;
        private Timer soapNeededTimer;
        private Timer soapNeededStopTimer;

        public event EventHandler IconsUpdated;

        private int handwashDelay = 3000; //15*1000
        private int handwashStop = 10000; //5*60*1000
        private int soapNeededDelay = 3000; //5*1000
        private int soapNeededStop = 10000; //1*60*1000
        private int handwashDiffTime = 15000; //5*60*1000

        //TODO datetime for events
        private DateTime lastHandwashDate;
        private DateTime lastSoapDate;
        private DateTime lastToothbrushDate;

        private void TriggerUpdate()
        {
            IconsUpdated?.Invoke(this, EventArgs.Empty);
        }

        private Timer CreateActiveTimer(int time, ElapsedEventHandler eventHandler)
        {
            var timer = new Timer(time);
            timer.AutoReset = false;
            timer.Elapsed += eventHandler;
            timer.Start();
            return timer;
        }

        public void ToiletEvent(BasicEvent evt)
        {
            toiletTimer?.Close();
            toiletTimer = CreateActiveTimer(handwashDelay, ToiletTimer_Elapsed);
        }

        private void ToiletTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            handwashStopTimer?.Close();
            HandwashIcon = true;
            handwashStopTimer = CreateActiveTimer(handwashStop, HandwashStopTimer_Elapsed);
            TriggerUpdate();
        }

        private void HandwashStopTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            HandwashIcon = false;
            TriggerUpdate();
        }

        private void SoapNeededTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _soapIcon = true;
            soapNeededStopTimer?.Close();
            soapNeededStopTimer = CreateActiveTimer(soapNeededStop, SoapNeededStopTimer_Elapsed);
            TriggerUpdate();
        }

        private void SoapNeededStopTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _soapIcon = false;
            TriggerUpdate();
        }

        public void SoapEvent(BasicEvent evt)
        {
            soapNeededTimer?.Close();
            soapNeededStopTimer?.Close();
            _soapIcon = false;
            TriggerUpdate();
        }

        public void HandwashEvent(BasicEvent evt)
        {
            double diff = (DateTime.Now - lastHandwashDate).TotalSeconds;
            if (lastHandwashDate != DateTime.MinValue && diff > handwashDiffTime/1000)
            {
                toiletTimer?.Close();
                handwashStopTimer?.Close();
                HandwashIcon = false;

                soapNeededTimer?.Close();
                soapNeededTimer = CreateActiveTimer(soapNeededDelay, SoapNeededTimer_Elapsed);

                lastHandwashDate = evt.EndTimeNet;

                TriggerUpdate();
            } else
            {
                lastHandwashDate = evt.EndTimeNet;
            }
        }

        public void ToothbrushEvent(BasicEvent evt)
        {

        }

    }
}
