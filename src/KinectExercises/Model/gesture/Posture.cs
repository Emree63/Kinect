using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.gesture
{
    public abstract class Posture : BaseGesture
    {
        public override void TestGesture(Body body)
        {
            if (TestPosture(body))
            {
                OnGestureRecognized();
            }
        }

        protected abstract bool TestPosture(Body body);
    }
}
