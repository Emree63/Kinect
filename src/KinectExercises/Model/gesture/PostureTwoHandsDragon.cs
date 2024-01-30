using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.gesture
{
    public class PostureTwoHandsDragon : Posture
    {
        private const float KNEE_FOOT_THRESHOLD = .2f;
        public override string GestureName => "Two hands dragon";
        protected override bool TestPosture(Body body)
        {
            if (
                body.Joints.TryGetValue(JointType.HandRight, out var handRight) &&
                body.Joints.TryGetValue(JointType.HandLeft, out var handLeft) &&
                body.Joints.TryGetValue(JointType.FootRight, out var footRight) &&
                body.Joints.TryGetValue(JointType.KneeLeft, out var kneeLeft) &&
                body.Joints.TryGetValue(JointType.Head, out var head))
            {
                return handRight.Position.Y > head.Position.Y &&
                    handLeft.Position.Y > head.Position.Y &&
                    handLeft.Position.X < handRight.Position.X &&
                    Math.Abs(kneeLeft.Position.Y - footRight.Position.Y) < KNEE_FOOT_THRESHOLD;
            }

            return false;

        }
    }
}
