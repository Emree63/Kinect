using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.gesture
{
    public abstract class BaseGesture
    {

        public event EventHandler<GestureRecognizedEventArgs> GestureRecognized;

        public string GestureName { get; private set; }

        public abstract void TestGesture(Body body);

        protected void OnGestureRecognized()
        {
            EventHandler<GestureRecognizedEventArgs> handler = GestureRecognized;
            if (null != handler) handler(this, new());
        }


    }
}
