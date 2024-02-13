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

        public abstract string GestureName { get; }

        public abstract void TestGesture(Body body);

        protected void OnGestureRecognized(object sender, GestureRecognizedEventArgs e)
        {
            EventHandler<GestureRecognizedEventArgs> handler = GestureRecognized;
            if (null != handler) handler(this, e);
        }


    }
}
