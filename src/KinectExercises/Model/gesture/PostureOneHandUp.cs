using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.gesture
{
    public class PostureOneHandUp : Posture
    {
        public override string GestureName => "One hand up";

        protected override bool TestPosture(Body body)
        {
            if (body.Joints.TryGetValue(JointType.HandRight, out var handR) &&
                body.Joints.TryGetValue(JointType.HandLeft, out var handL) &&
                body.Joints.TryGetValue(JointType.Head, out var head))
            {
                return handR.Position.Y > head.Position.Y ||
                       handL.Position.Y > head.Position.Y;
            }

            return false;
        }
    }
}
