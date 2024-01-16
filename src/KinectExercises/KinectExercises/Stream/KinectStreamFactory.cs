using Model;
using Model.Stream;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace KinectExercises.Stream
{
    public class KinectStreamFactory
    {
        static public KinectStream? BuildStream(KinectManager manager, WriteableBitmap bitmap, StreamType type)
        {
            switch(type)
            {
                case StreamType.ColorStream:
                    return new ColorImageStream(manager, bitmap);
                case StreamType.DepthStream:
                    return new DepthImageStream(manager, bitmap);
                case StreamType.InfraredStream:
                    return new InfraredImageStream(manager, bitmap);
                default: 
                    return null;
            }
        }
    }
}
