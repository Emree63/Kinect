using KinectExercises.Stream;
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

        private IDictionary<ulong, bool> bodies = new Dictionary<ulong, bool>();

        public override void TestGesture(Body body)
        {


            if (TestPosture(body))
            {
                if (!bodies.ContainsKey(body.TrackingId))
                {
                    bodies[body.TrackingId] = true;
                    OnGestureRecognized(this, new(body, this));
                }
            } else
            {
                bodies.Remove(body.TrackingId);
            }




        }

        protected abstract bool TestPosture(Body body);
    }
}
