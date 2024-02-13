using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.gesture
{
    public class PostureRightHand : Posture
    {
        private const float X_THRESHOLD = .5f;
        public override string GestureName => "Right hand";
        protected override bool TestPosture(Body body)
        {
            if (body.Joints.TryGetValue(JointType.HandRight, out var hand) &&
                body.Joints.TryGetValue(JointType.ElbowRight, out var elbow) &&
                body.Joints.TryGetValue(JointType.ShoulderRight, out var shoulder)
                )
            {
                return hand.Position.X > elbow.Position.X &&
                       elbow.Position.X > shoulder.Position.X &&
                       (shoulder.Position.X - hand.Position.X) < X_THRESHOLD;
            }

            return false;
        }
    }
}
