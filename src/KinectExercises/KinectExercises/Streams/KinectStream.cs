using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Kinect;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Model;

namespace Streams
{
    public abstract class KinectStream
    {
        protected KinectManager Manager { get; }
        protected KinectSensor Sensor { get => Manager.Sensor; }

        public KinectStream(KinectManager manager)
        {
            Manager = manager;
        }

        public void Start() => Manager.StartSensor();

        public void Stop() => Manager.StopSensor();
    }
}
