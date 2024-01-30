using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.gesture
{
    public static class GestureManager
    {
        private static BodyFrameReader bodyFrameReader = null;

        private static readonly List<BaseGesture> _KnownGestures = new();

        public static ReadOnlyCollection<BaseGesture> KnownGestures = new(_KnownGestures);
        public static KinectManager KinectManager { get; set; }
        

        public static event EventHandler<GestureRecognizedEventArgs> GestureRecognized;

        private static void OnGestureRecognized(object sender, GestureRecognizedEventArgs e)
        {
            EventHandler<GestureRecognizedEventArgs> handler = GestureRecognized;
            if (null != handler) handler(typeof(GestureManager), e);
        }

        public static void AddGestures(params BaseGesture[] gestures)
        {
            foreach (BaseGesture gesture in gestures)
            {
                gesture.GestureRecognized += OnGestureRecognized;
                _KnownGestures.Add(gesture);
            }
        }

        public static void RemoveGesture(BaseGesture gesture)
            => _KnownGestures.Remove(gesture);

        public static void StartAcquiringFrames(KinectManager manager)
        {
            bodyFrameReader = manager.Sensor.BodyFrameSource.OpenReader();
            bodyFrameReader.FrameArrived += Reader_BodyFrameArrived;
        }
        public static void StopAcquiringFrame()
        {
            bodyFrameReader.Dispose();
            bodyFrameReader = null;
        }

        private static void Reader_BodyFrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    var bodies = new Body[bodyFrame.BodyCount];
                    bodyFrame.GetAndRefreshBodyData(bodies);

                    foreach (Body body in bodies)
                    {
                        foreach (BaseGesture gesture in KnownGestures)
                        {
                            gesture.TestGesture(body);
                        }
                    }
                }
            }
        }

    }
}
