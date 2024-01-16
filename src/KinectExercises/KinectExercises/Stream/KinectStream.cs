using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Model.Stream
{
    public abstract class KinectStream
    {
        protected KinectManager Manager { get; }
        protected KinectSensor Sensor { get => Manager.Sensor; }
        protected WriteableBitmap Bitmap { get; }


        public KinectStream(KinectManager manager, WriteableBitmap bitmap)
        {
            Manager = manager;
        }

        public void Start() => Manager.StartSensor();

        public void Stop() => Manager.StopSensor();
    }
}
