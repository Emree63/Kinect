﻿using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.gesture
{
    public class PostureRightHandUp : Posture
    {
        public override string GestureName => "Right hand up";

        protected override bool TestPosture(Body body)
        {
            if (body.Joints.TryGetValue(JointType.HandRight, out var hand) &&
                body.Joints.TryGetValue(JointType.Head, out var head))
            {
                return hand.Position.Y > head.Position.Y;
            }

            return false;
        }
    }
}
