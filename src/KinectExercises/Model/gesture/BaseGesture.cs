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

        public event EventHandler GestureRecognized;

        public string GestureName { get; private set; }

        public abstract void TestGesture(Body body);

        protected void OnGestureRecognized()
        {
            EventHandler handler = GestureRecognized;
            if (null != handler) handler(this, EventArgs.Empty);
        }


    }
}
