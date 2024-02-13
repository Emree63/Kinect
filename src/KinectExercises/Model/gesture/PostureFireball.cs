using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.gesture
{
    public class PostureFireball : Posture
    {
        private const float Z_THRESHOLD = .3f;
        public override string GestureName => "Fireball";
        protected override bool TestPosture(Body body)
        {
            if (body.Joints.TryGetValue(JointType.HandRight, out var handR) &&
                body.Joints.TryGetValue(JointType.HandLeft, out var handL) &&
                body.Joints.TryGetValue(JointType.Head, out var head) && 
                body.Joints.TryGetValue(JointType.SpineBase, out var hips)
                )
            {
                return handR.Position.Y < head.Position.Y &&
                       handL.Position.Y < head.Position.Y &&
                       handR.Position.Y > hips.Position.Y &&
                       handL.Position.Y > hips.Position.Y &&
                       (hips.Position.Z - handR.Position.Z) > Z_THRESHOLD &&
                       (hips.Position.Z - handL.Position.Z) > Z_THRESHOLD;

            }

            return false;
        }
    }
}
