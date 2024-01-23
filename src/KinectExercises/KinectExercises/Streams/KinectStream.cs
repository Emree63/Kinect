using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Kinect;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Model.Stream
{
    public abstract class KinectStream
    {
        protected KinectManager Manager { get; }
        protected KinectSensor Sensor { get => Manager.Sensor; }
        abstract public ImageSource Source { get; }

        public KinectStream(KinectManager manager)
        {
            Manager = manager;
        }

        public void Start() => Manager.StartSensor();

        public void Stop() => Manager.StopSensor();
    }
}
