﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormVisualization
{
    class SimulationEvent
    {
        public const string Walking = "{\"timestamp\":1606387463,\"patientId\":\"2\",\"monitorId\":\"2\",\"sensorId\":\"AndroidStepCounter\",\"eventType\":\"OpenCare.EVODAY.EDL.WalkingDetected\",\"sensorType\":\"OpenCare.EVODAY.EDL.WalkingDetected\",\"deviceModel\":\"BeSafe\",\"value\":1277.0,\"value3\":3,\"unit\":\"steps\",\"unit3\":\"% active\",\"startTime\":1606387164,\"endTime\":1606387464}";
        public const string Soap = "{\"timestamp\":1606387485,\"patientId\":\"2\",\"monitorId\":\"2\",\"room\":\"Bathroom\",\"sensorId\":\"PressalitHandWash12\",\"eventType\":\"OpenCare.EVODAY.EDL.SoapDispensnig\",\"sensorType\":\"OpenCare.EVODAY.EDL.SoapDispensnig\",\"deviceModel\":\"SoapDetect\",\"deviceVendor\":\"Pressalit\",\"evenTypeEnum\":82035,\"value\":1.0,\"startTime\":1606387485,\"endTime\":1606387485,\"description\":\"Pressalit hand wash tracker\",\"TimeStampNet\":\"2020-11-26T10:44:45Z\",\"StartTimeNet\":\"2020-11-26T10:44:45Z\",\"EndTimeNet\":\"2020-11-26T10:44:45Z\"}";
        public const string HandWash = "{\"timestamp\":1606387505,\"patientId\":\"2\",\"monitorId\":\"2\",\"room\":\"Bathroom\",\"sensorId\":\"PressalitHandWash12\",\"eventType\":\"OpenCare.EVODAY.EDL.HandWashingEvent\",\"sensorType\":\"OpenCare.EVODAY.EDL.HandWashingEvent\",\"deviceModel\":\"SoapDetect\",\"deviceVendor\":\"Pressalit\",\"evenTypeEnum\":82202,\"value\":1.0,\"startTime\":1606387505,\"endTime\":1606387505,\"description\":\"Pressalit hand wash tracker\",\"TimeStampNet\":\"2020-11-26T10:45:05Z\",\"StartTimeNet\":\"2020-11-26T10:45:05Z\",\"EndTimeNet\":\"2020-11-26T10:45:05Z\"}";
        public const string UserIsHome = "{\"timestamp\": 1606387512, \"value\": 1, \"room\": \"Home\", \"rssi\": -75, \"startTime\": 1606387512, \"endTime\": 1606387602, \"directEvent\": true, \"patientId\": \"14051\", \"monitorId\": \"14051\", \"sensorId\": \"FF:FF:A4:4E:A0:80\", \"sensorType\": \"OpenCare.EVODAY.EDL.UserIsHomeEvent\", \"deviceModel\": \"Bluetooth Beacon\", \"deviceVendor\": \"\", \"gatewayId\": \"14051\"}";
        public const string HandWash2 = "{\"timestamp\":1606387527,\"patientId\":\"2\",\"monitorId\":\"2\",\"room\":\"Bathroom\",\"sensorId\":\"PressalitHandWash12\",\"eventType\":\"OpenCare.EVODAY.EDL.SoapDispensnig\",\"sensorType\":\"OpenCare.EVODAY.EDL.SoapDispensnig\",\"deviceModel\":\"SoapDetect\",\"deviceVendor\":\"Pressalit\",\"evenTypeEnum\":82035,\"value\":1.0,\"startTime\":1606387527,\"endTime\":1606387527,\"description\":\"Pressalit hand wash tracker\",\"TimeStampNet\":\"2020-11-26T10:45:27Z\",\"StartTimeNet\":\"2020-11-26T10:45:27Z\",\"EndTimeNet\":\"2020-11-26T10:45:27Z\"}";
        public const string UserIsHome2 = "{\"timestamp\": 1606387527, \"value\": 1, \"room\": \"Home\", \"rssi\": -77, \"eventType\": \"OpenCare.EVODAY.EDL.UserIsHomeEvent\", \"startTime\": 1606387527, \"endTime\": 1606387617, \"directEvent\": true, \"patientId\": \"14054\", \"monitorId\": \"14054\", \"sensorId\": \"FF:FF:BE:8B:C2:80\", \"sensorType\": \"OpenCare.EVODAY.EDL.UserIsHomeEvent\", \"deviceModel\": \"iTag\", \"deviceVendor\": \"AD\", \"gatewayId\": \"14054\"}";
        public const string HandWash3 = "{\"timestamp\":1606387549,\"patientId\":\"2\",\"monitorId\":\"2\",\"room\":\"Bathroom\",\"sensorId\":\"PressalitHandWash12\",\"eventType\":\"OpenCare.EVODAY.EDL.HandWashingEvent\",\"sensorType\":\"OpenCare.EVODAY.EDL.HandWashingEvent\",\"deviceModel\":\"SoapDetect\",\"deviceVendor\":\"Pressalit\",\"evenTypeEnum\":82202,\"value\":1.0,\"startTime\":1606387549,\"endTime\":1606387549,\"description\":\"Pressalit hand wash tracker\",\"TimeStampNet\":\"2020-11-26T10:45:49Z\",\"StartTimeNet\":\"2020-11-26T10:45:49Z\",\"EndTimeNet\":\"2020-11-26T10:45:49Z\"}";
        public const string HandWashing = "{\"timestamp\":1606387581,\"patientId\":\"2\",\"monitorId\":\"2\",\"room\":\"Bathroom\",\"sensorId\":\"PressalitHandWash12\",\"eventType\":\"OpenCare.EVODAY.EDL.HandWashingEvent\",\"sensorType\":\"OpenCare.EVODAY.EDL.HandWashingEvent\",\"deviceModel\":\"SoapDetect\",\"deviceVendor\":\"Pressalit\",\"evenTypeEnum\":82202,\"value\":1.0,\"startTime\":1606387581,\"endTime\":1606387581,\"description\":\"Pressalit hand wash tracker\",\"TimeStampNet\":\"2020-11-26T10:46:21Z\",\"StartTimeNet\":\"2020-11-26T10:46:21Z\",\"EndTimeNet\":\"2020-11-26T10:46:21Z\"}";
        public const string StepCounter = "{\"timestamp\":1606387683,\"patientId\":\"7\",\"monitorId\":\"7\",\"sensorId\":\"AndroidStepCounter\",\"eventType\":\"OpenCare.EVODAY.EDL.WalkingDetected\",\"sensorType\":\"OpenCare.EVODAY.EDL.WalkingDetected\",\"deviceModel\":\"BeSafe\",\"value\":615.0,\"value3\":0,\"unit\":\"steps\",\"unit3\":\"% active\",\"startTime\":1606387383,\"endTime\":1606387683}";
        public const string ToiletOcupied = "{\"timestamp\":1606387669,\"patientId\":\"2\",\"monitorId\":\"2\",\"room\":\"Bathroom\",\"sensorId\":\"PressalitToilet1_2\",\"eventType\":\"OpenCare.EVODAY.EDL.ToiletOccupancy\",\"sensorType\":\"OpenCare.EVODAY.EDL.ToiletOccupancy\",\"deviceModel\":\"ToiletDetect\",\"deviceVendor\":\"Pressalit\",\"evenTypeEnum\":82146,\"value\":1.0,\"length\":14,\"startTime\":1606387649,\"endTime\":1606387663,\"TimeStampNet\":\"2020-11-26T10:47:49Z\",\"StartTimeNet\":\"2020-11-26T10:47:29Z\",\"EndTimeNet\":\"2020-11-26T10:47:43Z\"}";
    }
}
