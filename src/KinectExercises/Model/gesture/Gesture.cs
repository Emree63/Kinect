using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.gesture
{
    public abstract class Gesture : BaseGesture
    {
        public bool IsTesting { get; set; }
        protected int MinNbOfFrames;
        protected int MaxNbOfFrames;
        private int CurrentFrameCount;

        protected abstract bool TestInitialConditions(Body body);
        protected abstract bool TestPosture(Body body);
        protected abstract bool TestRunningGesture(Body body);
        protected abstract bool TestEndConditions(Body body);
        public override void TestGesture(Body body)
        {

            if (CurrentFrameCount == 0)
            {
                if (TestInitialConditions(body))
                {
                    CurrentFrameCount++;
                }
            } else {
                if (TestPosture(body) && TestRunningGesture(body))
                {
                    CurrentFrameCount++;
                } 
                else if (TestEndConditions(body))
                {
                    if (CurrentFrameCount >= MinNbOfFrames && CurrentFrameCount <= MaxNbOfFrames)
                    {
                        OnGestureRecognized(this, new(body, this));
                    }
                    CurrentFrameCount = 0;
                }
                else
                {
                    CurrentFrameCount = 0;
                }
            }
        }

    }
}
