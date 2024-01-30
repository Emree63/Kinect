using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.gesture
{
    public class GestureRecognizedEventArgs : EventArgs
    {
        public Body Body;
        public BaseGesture Gesture;

        public GestureRecognizedEventArgs(Body body, BaseGesture gesture) 
        {
            Body = body;
            Gesture = gesture;
        }
    }
}
