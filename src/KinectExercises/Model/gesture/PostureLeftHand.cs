using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.gesture
{
    public class PostureLeftHand : Posture
    {
        private const float X_THRESHOLD = .5f;
        public override string GestureName => "Left hand";
        protected override bool TestPosture(Body body)
        {
            if (body.Joints.TryGetValue(JointType.HandLeft, out var hand) &&
                body.Joints.TryGetValue(JointType.ElbowLeft, out var elbow) &&
                body.Joints.TryGetValue(JointType.ShoulderLeft, out var shoulder)
                )
            {
                return hand.Position.X < elbow.Position.X &&
                    elbow.Position.X < shoulder.Position.X && 
                    (hand.Position.X - shoulder.Position.X) < X_THRESHOLD;
            }

            return false;
        }
    }
}
