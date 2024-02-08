using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.gesture
{
    public abstract class Gesture
    {
        public bool IsTesting { get; set; }
        protected int MinNbOfFrames { get; set; }
        protected int MaxNbOfFrames { get; set; }
        private int mCurrentFrameCount { get; set; }

        protected abstract bool TestInitialConditions(Body body);
        protected abstract bool TestPosture(Body body);
        protected abstract bool TestRunningGesture(Body body);
        protected abstract bool TestEndConditions(Body body);
        public abstract void TestGesture(Body body);
    }
}
