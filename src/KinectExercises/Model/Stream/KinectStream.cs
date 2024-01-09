using Microsoft.Kinect;
using System;
using System.Collections.Generic;
namespace Model.Stream
{
    public abstract class KinectStream
    {
        public KinectManager Manager { get; }

        public KinectSensor Sensor { get => Manager.Sensor; }

        public KinectStream(KinectManager manager)
        {
            Manager = manager;
        }

        public void Start() => Manager.StartSensor();

        public void Stop() => Manager.StopSensor();
    }
}
